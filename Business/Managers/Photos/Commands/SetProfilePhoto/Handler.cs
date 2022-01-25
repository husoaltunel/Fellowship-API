using Business.Entities.Concrete;
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

namespace Business.Managers.Photos.Commands.SetProfilePhoto
{
    public class Handler : BaseConnection, IRequestHandler<SetProfilePhotoCommand, IResult>
    {
        public Handler(IDbConnection connection)
        {
            Connection = connection;
        }
        public async Task<IResult> Handle(SetProfilePhotoCommand request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var user = await unitOfWork.DbContext.Users.GetByIdAsync(request.UserId);

                if (!ResultUtil<User>.IsDataExist(user))
                {
                    return new ErrorResult();
                }

                var oldAlbumProfilePhotoResult = await unitOfWork.DbContext.AlbumPhotos.GetByFilterAsync(ap => ap.AlbumId == user.AlbumId && ap.IsMain == true);
                var oldAlbumProfilePhoto = oldAlbumProfilePhotoResult.FirstOrDefault();
                var albumPhotoResult = await unitOfWork.DbContext.AlbumPhotos.GetByFilterAsync(ap => ap.AlbumId == user.AlbumId && ap.PhotoId == request.PhotoId);
                var albumPhoto = albumPhotoResult.FirstOrDefault();
               

                if (!ResultUtil<AlbumPhoto>.IsDataExist(albumPhoto))
                {
                    return new ErrorResult();
                }

                albumPhoto.IsMain = true;
                
                unitOfWork.BeginTransaction();

                try
                {
                    if (ResultUtil<AlbumPhoto>.IsDataExist(oldAlbumProfilePhoto))
                    {
                        oldAlbumProfilePhoto.IsMain = false;
                        await unitOfWork.DbContext.AlbumPhotos.UpdateAsync(oldAlbumProfilePhoto);
                    }
                    
                    await unitOfWork.DbContext.AlbumPhotos.UpdateAsync(albumPhoto);
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.RollBack();
                    return new ErrorResult();
                }

                return new SuccessResult();
            }
        }
    }
}
