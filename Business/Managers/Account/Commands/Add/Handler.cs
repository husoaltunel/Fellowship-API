using AutoMapper;
using Business.Concrete;
using Business.Constants;
using Business.Managers.Account.Queries.GetByUserName;
using Core.Utilities.Hashing;
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

namespace Business.Managers.Account.Commands.Add
{
    public class Handler : BaseConnection, IRequestHandler<UserAddCommand,IDataResult<UserViewDto>>
    {
        private IMapper _mapper;
        private IMediator _mediator;
        public Handler(IDbConnection dbConnection,IMapper mapper ,IMediator mediator)
        {
            Connection = dbConnection;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<IDataResult<UserViewDto>> Handle(UserAddCommand request, CancellationToken cancellationToken)
        {
            using(var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var user = await _mediator.Send(new UserGetByUserNameQuery() { Username = request.Username });
                if (ResultUtil<UserViewDto>.IsDataExist(user.Data))
                {
                    return new ErrorDataResult<UserViewDto>(message:UserMessages.UserExist);
                }
                HashingUtil.GeneratePasswordHashAndSalt(request.Password);
                var result = await unitOfWork.DbContext.Users.AddAsync(_mapper.Map<Core.Entities.Concrete.User>(request));
                if (ResultUtil<int>.IsResultSuccees(result))
                {
                    return await _mediator.Send(new UserGetByUserNameQuery(){ Username = request.Username });
                }
                return new ErrorDataResult<UserViewDto>();
            }
                       
        }
    }
}
