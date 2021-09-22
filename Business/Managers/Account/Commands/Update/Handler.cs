using AutoMapper;
using Business.Concrete;
using Core.Utilities.Hashing;
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

namespace Business.Managers.Account.Commands.Update
{
    public class Handler : BaseConnection, IRequestHandler<UserUpdateCommand, IDataResult<int>>
    {
        private readonly IMapper _mapper;
        public Handler(IDbConnection connection, IMapper mapper)
        {
            Connection = connection;
            _mapper = mapper;
        }
        public async Task<IDataResult<int>> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                HashingUtil.GeneratePasswordHashAndSalt(request.Password);
                var result = await unitOfWork.DbContext.Users.UpdateAsync(_mapper.Map<Core.Entities.Concrete.User>(request));
                if (ResultUtil<int>.IsResultSuccees(result))
                {
                    return new SuccessDataResult<int>(result);
                }
                return new ErrorDataResult<int>(result);
            }
        }
    }
}
