using AutoMapper;
using Business.Concrete;
using Business.Constants;
using Business.Managers.User.Queries.GetUserByUserName;
using Core.Utilities.Hashing;
using Core.Utilities.Hashing.Abstract;
using Core.Utilities.Results;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Utilities.UnitOfWork;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
                var newUser = new Core.Entities.Concrete.User()
                {
                    PasswordHash = _hashingHelper.GetPasswordHash(),
                    PasswordSalt = _hashingHelper.GetPasswordSalt()
                };
                newUser = _mapper.Map(request,newUser);
                var result = await unitOfWork.DbContext.Users.AddAsync(newUser);
                if (ResultUtil<int>.IsResultSuccees(result))
                {
                    return await _mediator.Send(new GetUserByUserNameQuery() { Username = request.Username });
                }
                return new ErrorDataResult<UserDto>();
            }

        }
    }
}
