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

namespace Business.Managers.UserLikes.Commands.DeleteUserLike
{
    public class Handler : BaseConnection, IRequestHandler<DeleteUserLikeCommand, IResult>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public Handler(IDbConnection connection, IMediator mediator, IMapper mapper)
        {
            Connection = connection;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<IResult> Handle(DeleteUserLikeCommand request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var userLikeFilterResult = await unitOfWork.DbContext.UserLikes.GetByFilterAsync(userLike => userLike.AcclaimedUserId == request.AcclaimedUserId && userLike.UserLikedId == request.UserLikedId);
                var userLike = userLikeFilterResult.FirstOrDefault();

                if (!ResultUtil<UserLike>.IsDataExist(userLike))
                {
                    return new ErrorResult();
                }

                var  isDeleted = await unitOfWork.DbContext.UserLikes.DeleteAsync(userLike.Id);

                if (!ResultUtil<int>.IsResultSuccees(isDeleted))
                {
                    return new ErrorResult();
                }

                return new SuccessResult();
            }
        }
    }
}
