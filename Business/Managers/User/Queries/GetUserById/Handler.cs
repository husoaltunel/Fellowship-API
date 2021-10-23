using AutoMapper;
using Business.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Utilities.UnitOfWork;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Managers.User.Queries.GetUserById
{
    public class Handler : BaseConnection, IRequestHandler<GetUserByIdQuery, IDataResult<UserDto>>
    {
        private IMapper _mapper;
        public Handler(IDbConnection connection,IMapper mapper)
        {
            Connection = connection;
            _mapper = mapper;
        }
        public async Task<IDataResult<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            using(var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var taskResult = await unitOfWork.DbContext.Users.GetByIdAsync(request.Id);

                if (ResultUtil<Core.Entities.Concrete.User>.IsDataExist(taskResult))
                {
                    return new SuccessDataResult<UserDto>(_mapper.Map<UserDto>(taskResult));
                }
                return new ErrorDataResult<UserDto>();
            }
            
        }
    }
}
