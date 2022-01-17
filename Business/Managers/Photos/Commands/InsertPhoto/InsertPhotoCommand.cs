using Business.Utilities.Results.Abstract;
using Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.Photos.Commands.InsertPhoto
{
    public class InsertPhotoCommand : PhotoDto, IRequest<IDataResult<int>>
    {

    }
}
