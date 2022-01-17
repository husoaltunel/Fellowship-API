using AutoMapper;
using Business.Constants;
using Business.Managers.Users.Queries.GetUserByUserName;
using Business.Entities.Concrete;
using Business.Utilities.Hashing.Abstract;
using Business.Utilities.Results;
using Business.Utilities.Results.Abstract;
using Business.Utilities.Results.Concrete;
using DataAccess.Utilities.UnitOfWork;
using Entities.Dtos;
using MediatR;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.DataAccess.Concrete;

namespace Business.Managers.Auth.Commands.Register
{
    public class Handler : BaseConnection, IRequestHandler<RegisterCommand, IDataResult<UserDto>>
    {
        private IMapper _mapper;
        private IMediator _mediator;
        private IHashingHelper _hashingHelper;
        public Handler(IDbConnection dbConnection, IMapper mapper, IMediator mediator, IHashingHelper hashingHelper)
        {
            Connection = dbConnection;
            _mapper = mapper;
            _mediator = mediator;
            _hashingHelper = hashingHelper;
        }
        public async Task<IDataResult<UserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var user = await _mediator.Send(new GetUserByUserNameQuery() { Username = request.Username });
                if (ResultUtil<UserDto>.IsDataExist(user.Data))
                {
                    return new ErrorDataResult<UserDto>(message: AuthMessages.UserExist);
                }
                _hashingHelper.GeneratePasswordHashAndSalt(request.Password);
                var newUser = new User()
                {
                    PasswordHash = _hashingHelper.GetPasswordHash(),
                    PasswordSalt = _hashingHelper.GetPasswordSalt()
                };
                newUser = _mapper.Map(request,newUser);
                var result = await unitOfWork.DbContext.Users.AddAsync(newUser);
                if (!ResultUtil<int>.IsResultSuccees(result))
                {
                    return new ErrorDataResult<UserDto>();
                   
                }
                return await _mediator.Send(new GetUserByUserNameQuery() { Username = request.Username });
            }

        }
    }
}
