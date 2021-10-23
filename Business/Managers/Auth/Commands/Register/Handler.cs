using AutoMapper;
using Business.Concrete;
using Business.Constants;
using Business.Managers.User.Queries.GetUserByUserName;
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

namespace Business.Managers.Auth.Commands.Register
{
    public class Handler : BaseConnection, IRequestHandler<RegisterCommand,IDataResult<UserDto>>
    {
        private IMapper _mapper;
        private IMediator _mediator;
        public Handler(IDbConnection dbConnection,IMapper mapper ,IMediator mediator)
        {
            Connection = dbConnection;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<IDataResult<UserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            using(var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var user = await _mediator.Send(new GetUserByUserNameQuery() { Username = request.Username });
                if (ResultUtil<UserDto>.IsDataExist(user.Data))
                {
                    return new ErrorDataResult<UserDto>(message:AuthMessages.UserExist);
                }
                HashingUtil.GeneratePasswordHashAndSalt(request.Password);
                var result = await unitOfWork.DbContext.Users.AddAsync(_mapper.Map<Core.Entities.Concrete.User>(request));
                if (ResultUtil<int>.IsResultSuccees(result))
                {
                    return await _mediator.Send(new GetUserByUserNameQuery(){ Username = request.Username });
                }
                return new ErrorDataResult<UserDto>();
            }
                       
        }
    }
}
