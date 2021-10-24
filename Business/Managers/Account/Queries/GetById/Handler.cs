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

namespace Business.Managers.Account.Queries.GetById
{
    public class Handler : BaseConnection, IRequestHandler<UserGetByIdQuery, IDataResult<UserViewDto>>
    {
        private IMapper _mapper;
        public Handler(IDbConnection connection,IMapper mapper)
        {
            Connection = connection;
            _mapper = mapper;
        }
        public async Task<IDataResult<UserViewDto>> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
        {
            using(var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var taskResult = await unitOfWork.DbContext.Users.GetById(request.Id);

                if (ResultUtil<Core.Entities.Concrete.User>.IsDataExist(taskResult))
                {
                    return new SuccessDataResult<UserViewDto>(_mapper.Map<UserViewDto>(taskResult));
                }
                return new ErrorDataResult<UserViewDto>();
            }
            
        }
    }
}
