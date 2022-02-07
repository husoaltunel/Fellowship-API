using AutoMapper;
using Business.Entities.Concrete;
using Business.Managers.Photos.Commands.DeletePhotoById;
using Business.Utilities.Results;
using Business.Utilities.Results.Abstract;
using Business.Utilities.Results.Concrete;
using Core.DataAccess.Concrete;
using DataAccess.Utilities.UnitOfWork;
using Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Managers.UserLikes.Commands.InsertUserLike
{
    public class Handler : BaseConnection, IRequestHandler<InsertUserLikeCommand, IResult>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public Handler(IDbConnection connection,IMediator mediator, IMapper mapper)
        {
            Connection = connection;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<IResult> Handle(InsertUserLikeCommand request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var userLiked = await unitOfWork.DbContext.Users.GetByIdAsync(request.AcclaimedUserId);
                var userLikeFilterResult = await unitOfWork.DbContext.UserLikes.GetByFilterAsync(userLike => userLike.AcclaimedUserId == request.AcclaimedUserId && userLike.UserLikedId == request.UserLikedId);
                var userLike = userLikeFilterResult.FirstOrDefault();

                if (!ResultUtil<User>.IsDataExist(userLiked))
                {
                    return new ErrorResult();
                }

                if (ResultUtil<UserLike>.IsDataExist(userLike))
                {
                    return new ErrorResult();
                }
                
                var insertedUserLike =  await unitOfWork.DbContext.UserLikes.InsertAsync(_mapper.Map<UserLike>(request));

                if (!ResultUtil<UserLike>.IsResultSuccees(insertedUserLike))
                {
                    return new ErrorResult();
                }
               
                return new SuccessResult();
            }
        }
    }
}
