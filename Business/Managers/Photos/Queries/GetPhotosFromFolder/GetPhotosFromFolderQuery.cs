using Entities.Concrete;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.Photos.Queries.GetPhotosFromFolder
{
    public class GetPhotosFromFolderQuery : IRequest<List<FileContentResult>>
    {
        public List<Photo> Photos { get;set;}
    }
}
