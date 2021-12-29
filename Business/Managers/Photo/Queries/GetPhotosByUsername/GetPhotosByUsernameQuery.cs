using Core.Utilities.Results.Abstract;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Business.Managers.Photo.Queries.GetPhotosByUsername
{
    public class GetPhotosByUsernameQuery : IRequest<IDataResult<IEnumerable<FileContentResult>>>
    {
        public string Username { get;set;}
        
    }
}
