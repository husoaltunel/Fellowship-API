using Business.Utilities.Results.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.Photos.Commands.DeletePhotosFromFolder
{
    public class DeletePhotosFromFolderCommand : IRequest<IResult>
    {
        public List<string> PhotoPaths { get;set;}
    }
}
