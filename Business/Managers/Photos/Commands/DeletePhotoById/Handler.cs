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

namespace Business.Managers.Photos.Commands.DeletePhotoById
{
    public class Handler : BaseConnection, IRequestHandler<DeletePhotoByIdCommand, IResult>
    {
        public Handler(IDbConnection connection)
        {
            Connection = connection;
        }
        public async Task<IResult> Handle(DeletePhotoByIdCommand request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var user = await unitOfWork.DbContext.Users.GetByIdAsync(int.Parse(request.UserId));
                var albumPhotoFilterResult = await unitOfWork.DbContext.AlbumPhotos.GetByFilterAsync(albumPhoto => albumPhoto.PhotoId == request.Id && albumPhoto.AlbumId == user.AlbumId);
                var albumPhoto = albumPhotoFilterResult.First();
                if (!ResultUtil<AlbumPhoto>.IsDataExist(albumPhoto))
                {
                    return new ErrorResult();
                }

                albumPhoto.IsDeleted = true;
                await unitOfWork.DbContext.AlbumPhotos.UpdateAsync(albumPhoto);

                return new SuccessResult();
            }
        }
    }
}
