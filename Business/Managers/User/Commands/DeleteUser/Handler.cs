using Business.Concrete;
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

namespace Business.Managers.User.Commands.DeleteUser
{
    public class Handler : BaseConnection, IRequestHandler<DeleteUserCommand, IDataResult<int>>
    {
        public Handler(IDbConnection connection)
        {
            Connection = connection;
        }
        public async Task<IDataResult<int>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var result = await unitOfWork.DbContext.Users.DeleteAsync(request.Id);
                if (ResultUtil<int>.IsResultSuccees(result))
                {
                    return new SuccessDataResult<int>(result);
                }
                return new ErrorDataResult<int>(result);
            }
        }
    }
}
