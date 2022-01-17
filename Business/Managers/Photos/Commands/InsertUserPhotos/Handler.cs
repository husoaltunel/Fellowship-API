using AutoMapper;
using Business.Managers.Photos.Commands.DeletePhotosFromFolder;
using Business.Managers.Photos.Commands.SavePhotosToFolder;
using Business.Managers.Users.Commands.UpdateUser;
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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Managers.Photos.Commands.InsertUserPhotos
{
    public class Handler : BaseConnection, IRequestHandler<InsertUserPhotosCommand, IResult>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public Handler(IMediator mediator, IDbConnection connection, IMapper mapper)
        {
            _mediator = mediator;
            Connection = connection;
            _mapper = mapper;
        }
        public async Task<IResult> Handle(InsertUserPhotosCommand request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                unitOfWork.BeginTransaction();

                List<string> savedPhotoUrls = new List<string>();

                try
                {
                    var user = await unitOfWork.DbContext.Users.GetByIdAsync(int.Parse(request.UserId));
                    int albumId = user.AlbumId;
                    

                    if (!ResultUtil<int>.IsDataExist(albumId))
                    {
                        var insertedAlbumId = await unitOfWork.DbContext.Albums.AddAsync(new Album());

                        if (ResultUtil<int>.IsDataExist(insertedAlbumId))
                        {
                            user.AlbumId = insertedAlbumId;
                            await _mediator.Send(_mapper.Map<UpdateUserCommand>(user));
                            albumId = insertedAlbumId;
                        }

                    }

                    if (ResultUtil<int>.IsDataExist(albumId))
                    {
                        savedPhotoUrls = await _mediator.Send(new SavePhotosToFolderCommand() { Photos = request.PhotoFiles });
                        foreach (var url in savedPhotoUrls)
                        {
                            var insertedPhotoId = await unitOfWork.DbContext.Photos.AddAsync( new Photo(){ Url = url });
                            await unitOfWork.DbContext.AlbumPhotos.AddAsync(new AlbumPhoto { AlbumId = albumId, PhotoId = insertedPhotoId });
                        }

                    }

                    unitOfWork.Commit();

                    return new SuccessResult();
                }
                catch
                {
                    _mediator.Send(new DeletePhotosFromFolderCommand(){ PhotoPaths = savedPhotoUrls });
                    unitOfWork.RollBack();                    
                }

                

                return new ErrorResult();

            }



        }
    }
}
