using AutoMapper;
using Business.Entities.Concrete;
using Business.Utilities.Results;
using Business.Utilities.Results.Abstract;
using Business.Utilities.Results.Concrete;
using Core.DataAccess.Concrete;
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

namespace Business.Managers.Users.Queries.GetUsers
{
    public class Handler : BaseConnection, IRequestHandler<GetUsersQuery, IDataResult<IEnumerable<UserDto>>>
    {
        private IMapper _mapper;
        public Handler(IDbConnection dbConnection,IMapper mapper)
        {
            Connection = dbConnection;
            _mapper = mapper;
        }
        public async Task<IDataResult<IEnumerable<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            using(var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var taskResult = await unitOfWork.DbContext.Users.GetAllAsync();

                if (ResultUtil<IEnumerable<User>>.IsDataExist(taskResult))
                {
                    var result = taskResult.Select(user => _mapper.Map<UserDto>(user));
                    return new SuccessDataResult<IEnumerable<UserDto>>(result);
                }
                return new ErrorDataResult<IEnumerable<UserDto>>();
            }
                     
        }
    }
}
