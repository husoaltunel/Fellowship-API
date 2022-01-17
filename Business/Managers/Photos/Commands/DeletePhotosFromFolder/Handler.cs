using Business.Utilities.Results.Abstract;
using Business.Utilities.Results.Concrete;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Managers.Photos.Commands.DeletePhotosFromFolder
{
    public class Handler : IRequestHandler<DeletePhotosFromFolderCommand, IResult>
    {
        private readonly IConfiguration _configuration;

        public Handler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IResult> Handle(DeletePhotosFromFolderCommand request, CancellationToken cancellationToken)
        {
            foreach (var path in request.PhotoPaths)
            {
                string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), _configuration.GetSection("ImagesFolderPath").Value, path);
                File.Delete(filePath);
            }

            return await Task.FromResult(new SuccessResult());
        }
    }
}
