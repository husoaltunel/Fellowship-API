using Core.Utilities.Results.Abstract;
using Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.Photo.Commands.InsertPhoto
{
    public class InsertUserPhotosCommand :InsertUserPhotosDto, IRequest<IDataResult<int>>
    {
        
    }
}
