using Business.Utilities.Results.Abstract;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.AlbumPhotos.Commands.InsertPhotoToAlbum
{
    public class InsertPhotoToAlbumCommand : AlbumPhotoDto, IRequest<IDataResult<int>>
    {

    }
}
