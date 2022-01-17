using Business.Managers.Photos.Queries.GetPhotosFromFolder;
using Business.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Managers.Photos.Commands.SavePhotosToFolder
{
    public class Handler : IRequestHandler<SavePhotosToFolderCommand, List<string>>
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator ;
        public Handler(IConfiguration configuration , IMediator mediator)
        {
            _configuration = configuration;
            _mediator = mediator;
        }
        public Task<List<string>> Handle(SavePhotosToFolderCommand request, CancellationToken cancellationToken)
        {
            List<string> newFileNames = new List<string>();

            if (ResultUtil<IFormFileCollection>.IsDataExist(request.Photos.Files))
            {
                foreach (var file in request.Photos.Files)
                {
                    string fileName = String.Concat(Guid.NewGuid().ToString(),file.FileName);
                    newFileNames.Add(fileName);
                    var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), _configuration.GetSection("ImagesFolderPath").Value,fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
                return Task.FromResult(newFileNames);
            }


            return Task.FromResult(newFileNames);
        }
    }
}
