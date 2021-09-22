using AutoMapper;
using Business.Concrete;
using Core.DataAccess.Abstract;
using Core.Utilities.Results;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Concrete;
using DataAccess.Concrete.Dapper;
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

namespace Business.Managers.Account.Queries.GetAll
{
    public class Handler : BaseConnection, IRequestHandler<UserGetAllQuery, IDataResult<IEnumerable<UserViewDto>>>
    {
        private IMapper _mapper;
        public Handler(IDbConnection dbConnection,IMapper mapper)
        {
            Connection = dbConnection;
            _mapper = mapper;
        }
        public async Task<IDataResult<IEnumerable<UserViewDto>>> Handle(UserGetAllQuery request, CancellationToken cancellationToken)
        {
            using(var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var taskResult = await unitOfWork.DbContext.Users.GetAllAsync();

                if (ResultUtil<IEnumerable<Core.Entities.Concrete.User>>.IsDataExist(taskResult))
                {
                    var result = taskResult.Select(user => _mapper.Map<UserViewDto>(user));
                    return new SuccessDataResult<IEnumerable<UserViewDto>>(result);
                }
                return new ErrorDataResult<IEnumerable<UserViewDto>>();
            }
                     
        }
    }
}
