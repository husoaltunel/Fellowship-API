using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.Photo.Queries.GetPhotosFromFolder
{
    public class GetPhotosFromFolderQuery : IRequest<List<FileContentResult>>
    {
        public List<Entities.Concrete.Photo> Photos { get;set;}
    }
}
