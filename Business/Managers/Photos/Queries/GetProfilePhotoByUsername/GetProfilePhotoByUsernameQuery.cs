using Business.Utilities.Results.Abstract;
using Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.Photos.Queries.GetProfilePhotoByUsername
{
    public class GetProfilePhotoByUsernameQuery : IRequest<IDataResult<PhotoDto>>
    {
        public string Username { get;set;}
    }
}
