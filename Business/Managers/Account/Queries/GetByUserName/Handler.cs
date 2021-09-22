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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Managers.Account.Queries.GetByUserName
{
    public class Handler : BaseConnection, IRequestHandler<UserGetByUserNameQuery, IDataResult<UserViewDto>>
    {
        private IMapper _mapper;
        public Handler(IDbConnection connection,IMapper mapper)
        {
            Connection = connection;
            _mapper = mapper;
        }
        public async Task<IDataResult<UserViewDto>> Handle(UserGetByUserNameQuery request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var query = await unitOfWork.DbContext.Users.GetByFilter(user => user.Username == request.Username.ToLower());
                var result = query.FirstOrDefault();
                if (ResultUtil<Core.Entities.Concrete.User>.IsDataExist(result))
                {
                    return new SuccessDataResult<UserViewDto>(_mapper.Map<UserViewDto>(result));
                }
                return new ErrorDataResult<UserViewDto>();
            }
        }
    }
}
