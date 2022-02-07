using AutoMapper;
using Business.Entities.Concrete;
using Business.Managers.Photos.Queries.GetPhotosFromFolder;
using Business.Managers.UserLikes.Queries.GetUserLikesByUsername;
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

namespace Business.Managers.UserLikes.Queries.DoILikeTheUser
{
    public class Handler : BaseConnection, IRequestHandler<DoILikeTheUserQuery, IResult>
    {
        public Handler(IDbConnection dbConnection, IConfiguration configuration, IMediator mediator, IMapper mapper)
        {
            Connection = dbConnection;
        }
        public async Task<IResult> Handle(DoILikeTheUserQuery request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var userLikeFilter = await unitOfWork.DbContext.UserLikes.GetByFilterAsync(userLike => userLike.UserLikedId == request.UserId && userLike.AcclaimedUserId == request.AcclaimedUserId);
                var acclaimedUser = userLikeFilter.FirstOrDefault();

                if (!ResultUtil<UserLike>.IsDataExist(acclaimedUser))
                {
                    return new ErrorResult();
                }

                return new SuccessResult();

            }
        }
    }
}
