using AutoMapper;
using Business.Entities.Concrete;
using Business.Managers.Photos.Queries.GetPhotosFromFolder;
using Business.Utilities.Results;
using Business.Utilities.Results.Abstract;
using Business.Utilities.Results.Concrete;
using Core.DataAccess.Concrete;
using DataAccess.Utilities.UnitOfWork;
using Entities.Concrete;
using Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Managers.UserLikes.Queries.GetUserLikesByUsername
{
    public class Handler : BaseConnection, IRequestHandler<GetUserLikesByUsernameQuery, IDataResult<UserLikeDto>>
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public Handler(IDbConnection dbConnection, IConfiguration configuration, IMediator mediator, IMapper mapper)
        {
            Connection = dbConnection;
            _configuration = configuration;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<IDataResult<UserLikeDto>> Handle(GetUserLikesByUsernameQuery request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var user = await unitOfWork.DbContext.Users.GetByIdAsync(request.UserId);

                if (!ResultUtil<User>.IsDataExist(user))
                {
                    return new ErrorDataResult<UserLikeDto>();
                }

                var userLikeDto = new UserLikeDto();
                var usersLikedMe = await unitOfWork.DbContext.UserLikes.GetByFilterAsync(userLike => userLike.AcclaimedUserId == user.Id);
                var usersILike = await unitOfWork.DbContext.UserLikes.GetByFilterAsync(userLike => userLike.UserLikedId == user.Id);

                if (ResultUtil<IEnumerable<UserLike>>.IsDataExist(usersLikedMe))
                {
                    foreach (var userLike in usersLikedMe)
                    {
                        var userLikedMe = await unitOfWork.DbContext.Users.GetByIdAsync(userLike.UserLikedId);
                        userLikeDto.UsersLikedMe.Add(_mapper.Map<UserDto>(userLikedMe)); 
                    }

                }
                if (ResultUtil<IEnumerable<UserLike>>.IsDataExist(usersILike))
                {
                    foreach (var userLike in usersILike)
                    {
                        var userILike = await unitOfWork.DbContext.Users.GetByIdAsync(userLike.AcclaimedUserId);
                        userLikeDto.UsersILiked.Add(_mapper.Map<UserDto>(userILike));
                    }

                }

                return new SuccessDataResult<UserLikeDto>(userLikeDto);
            }
        }
    }
}
