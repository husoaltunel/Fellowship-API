using Business.Utilities.Results;
using Business.Utilities.Results.Abstract;
using Business.Utilities.Results.Concrete;
using Core.DataAccess.Concrete;
using DataAccess.Utilities.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Managers.Users.Commands.DeleteUser
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
