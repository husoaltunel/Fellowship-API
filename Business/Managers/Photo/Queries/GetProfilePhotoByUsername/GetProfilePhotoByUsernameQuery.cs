using Core.Utilities.Results.Abstract;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.Photo.Queries.GetProfilePhotoByUsername
{
    public class GetProfilePhotoByUsernameQuery : IRequest<IDataResult<FileContentResult>>
    {
        public string Username { get;set;}
    }
}
