using Business.Utilities.Results.Abstract;
using Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Business.Managers.Photos.Queries.GetPhotosByUsername
{
    public class GetPhotosByUsernameQuery : IRequest<IDataResult<IEnumerable<PhotoDto>>>
    {
        public string Username { get;set;}
        
    }
}
