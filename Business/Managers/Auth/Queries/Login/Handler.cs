using AutoMapper;
using Business.Constants;
using Business.Managers.Users.Queries.GetUserByUserName;
using Business.Entities.Concrete;
using Business.Utilities.Hashing;
using Business.Utilities.Hashing.Abstract;
using Business.Utilities.Results;
using Business.Utilities.Results.Abstract;
using Business.Utilities.Results.Concrete;
using Business.Utilities.Security.Abstract;
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
using Core.DataAccess.Concrete;

namespace Business.Managers.Auth.Queries.Login
{
    public class Handler : BaseConnection, IRequestHandler<LoginQuery, IDataResult<LoginInfoDto>>
    {
        private IMapper _mapper { get; set; }
        private readonly IMediator _mediator;
        private readonly ITokenHelper _tokenHelper;
        private readonly IHashingHelper _hashingHelper;
        public Handler(IMapper mapper, IMediator mediator, ITokenHelper tokenHelper,IDbConnection connection,IHashingHelper hashingHelper)
        {
            _mapper = mapper;
            _mediator = mediator;
            _tokenHelper = tokenHelper;
            Connection = connection;
            _hashingHelper = hashingHelper;
        }
        public async Task<IDataResult<LoginInfoDto>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var result = await unitOfWork.DbContext.Users.GetByFilterAsync(user => user.Username == request.Username);
                var user = result.FirstOrDefault();
                if (!ResultUtil<User>.IsDataExist(user))
                {
                    return new ErrorDataResult<LoginInfoDto>(message: AuthMessages.UserNotFound);
                }
                if (!_hashingHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return new ErrorDataResult<LoginInfoDto>(message: AuthMessages.InvalidPassword);
                }
                var mappedData = _mapper.Map<LoginInfoDto>(user);
                mappedData.AccessToken = _tokenHelper.CreateToken(_mapper.Map<User>(user), new List<OperationClaim>());
                return new SuccessDataResult<LoginInfoDto>(mappedData,message:AuthMessages.SuccessLogin);
            }          

        }
    }
}
