using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.Photos.Commands.SavePhotosToFolder
{
    public class SavePhotosToFolderCommand : IRequest<List<string>>
    {
        public IFormCollection Photos { get;set;}
    }
}
