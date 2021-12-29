﻿using AutoMapper;
using Business.Concrete;
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
using System.Linq;
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
                var result = await unitOfWork.DbContext.Users.GetByFilterAsync(user => user.Username == request.Username);
                var user = result.FirstOrDefault();
                var updatedUser = _mapper.Map(request,user );

                if (!String.IsNullOrEmpty(request.Password))
                {
                    _hashingHelper.GeneratePasswordHashAndSalt(request.Password);
                    user.PasswordHash = _hashingHelper.GetPasswordHash();
                    user.PasswordSalt = _hashingHelper.GetPasswordSalt();
                }
                var updateResult = await unitOfWork.DbContext.Users.UpdateAsync(updatedUser);

                if (ResultUtil<int>.IsResultSuccees(updateResult))
                {
                    return new SuccessDataResult<int>(updateResult);
                }
                return new ErrorDataResult<int>(updateResult);
            }
        }
    }
}
