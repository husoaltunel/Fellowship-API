using Business.Concrete;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
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

namespace Business.Managers.Account.Queries.GetByUserNameForLogin
{
    public class Handler : BaseConnection, IRequestHandler<UserGetByUserNameForLoginQuery, IDataResult<User>>
    {
        public Handler(IDbConnection connection)
        {
            Connection = connection;
        }
        public async Task<IDataResult<User>> Handle(UserGetByUserNameForLoginQuery request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var result = await unitOfWork.DbContext.Users.GetByFilter(user => user.Username == request.Username);
                var user = result.FirstOrDefault();
                if (ResultUtil<User>.IsDataExist(user))
                {
                    return new SuccessDataResult<User>(user);
                }
                return new ErrorDataResult<User>();
            }
        }
    }
}
