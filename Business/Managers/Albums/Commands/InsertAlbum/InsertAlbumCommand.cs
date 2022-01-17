using Business.Utilities.Results.Abstract;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.Albums.Commands.InsertAlbum
{
    public class InsertAlbumCommand : AlbumDto, IRequest<IDataResult<int>>
    {

    }
}
