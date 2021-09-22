using AutoMapper;
using Business.Concrete;
using Business.Constants;
using Business.Managers.Account.Queries.GetByUserName;
using Business.Managers.Account.Queries.GetByUserNameForLogin;
using Core.Entities.Concrete;
using Core.Utilities.Hashing;
using Core.Utilities.Results;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Abstract;
using DataAccess.Utilities.UnitOfWork;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Managers.Account.Queries.Login
{
    public class Handler : BaseConnection, IRequestHandler<UserLoginQuery, IDataResult<UserLoginViewDto>>
    {
        private IMapper _mapper { get; set; }
        private readonly IMediator _mediator;
        private readonly ITokenHelper _tokenHelper;
        public Handler(IMapper mapper, IMediator mediator, ITokenHelper tokenHelper,IDbConnection connection)
        {
            _mapper = mapper;
            _mediator = mediator;
            _tokenHelper = tokenHelper;
            Connection = connection;
        }
        public async Task<IDataResult<UserLoginViewDto>> Handle(UserLoginQuery request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var result = await _mediator.Send(new UserGetByUserNameForLoginQuery(){Username = request.Username });
                var user = result.Data;
                if (!ResultUtil<User>.IsDataExist(user))
                {
                    return new ErrorDataResult<UserLoginViewDto>(message: UserMessages.UserNotFound);
                }
                if (!HashingUtil.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return new ErrorDataResult<UserLoginViewDto>(message: UserMessages.InvalidPassword);
                }
                var mappedData = _mapper.Map<UserLoginViewDto>(user);
                mappedData.AccessToken = _tokenHelper.CreateToken(_mapper.Map<User>(user), new List<OperationClaim>());
                return new SuccessDataResult<UserLoginViewDto>(mappedData);
            }          

        }
    }
}
