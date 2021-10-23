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

namespace Business.Managers.User.Queries.GetUserByUserNameForLogin
{
    public class Handler : BaseConnection, IRequestHandler<GetUserByUserNameForLoginQuery, IDataResult<Core.Entities.Concrete.User>>
    {
        public Handler(IDbConnection connection)
        {
            Connection = connection;
        }
        public async Task<IDataResult<Core.Entities.Concrete.User>> Handle(GetUserByUserNameForLoginQuery request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var result = await unitOfWork.DbContext.Users.GetByFilterAsync(user => user.Username == request.Username);
                var user = result.FirstOrDefault();
                if (ResultUtil<Core.Entities.Concrete.User>.IsDataExist(user))
                {
                    return new SuccessDataResult<Core.Entities.Concrete.User>(user);
                }
                return new ErrorDataResult<Core.Entities.Concrete.User>();
            }
        }
    }
}
