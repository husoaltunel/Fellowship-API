using AutoMapper;
using Business.Concrete;
using Business.Managers.User.Queries.GetUserFullInfoByUserName;
using Core.Extensions;
using Core.Utilities.Hashing;
using Core.Utilities.Hashing.Abstract;
using Core.Utilities.Results;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Utilities.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Managers.User.Commands.UpdateUser
{
    public class Handler : BaseConnection, IRequestHandler<UpdateUserCommand, IDataResult<int>>
    {
        private readonly IMapper _mapper;
        private readonly IHashingHelper _hashingHelper;
        private readonly IMediator _mediator;
        public Handler(IDbConnection connection, IMapper mapper, IHashingHelper hashingHelper, IMediator mediator)
        {
            Connection = connection;
            _mapper = mapper;
            _hashingHelper = hashingHelper;
            _mediator = mediator;
        }
        public async Task<IDataResult<int>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var user = await _mediator.Send(new GetUserFullInfoByUserNameQuery() { Username = request.Username });
                var updatedUser = _mapper.Map(request, user.Data);

                if (!String.IsNullOrEmpty(request.Password))
                {
                    _hashingHelper.GeneratePasswordHashAndSalt(request.Password);
                    user.Data.PasswordHash = _hashingHelper.GetPasswordHash();
                    user.Data.PasswordSalt = _hashingHelper.GetPasswordSalt();
                }
                var result = await unitOfWork.DbContext.Users.UpdateAsync(updatedUser);

                if (ResultUtil<int>.IsResultSuccees(result))
                {
                    return new SuccessDataResult<int>(result);
                }
                return new ErrorDataResult<int>(result);
            }
        }
    }
}
