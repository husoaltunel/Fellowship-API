using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Managers.Photo.Queries.GetPhotosFromFolder
{
    public class Handler : IRequestHandler<GetPhotosFromFolderQuery, List<FileContentResult>>
    {
        private readonly IConfiguration _configuration;

        public Handler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<FileContentResult>> Handle(GetPhotosFromFolderQuery request, CancellationToken cancellationToken)
        {
            var photoFiles = new List<FileContentResult>();

            request.Photos.ForEach(photo =>
            {
                var photoPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), _configuration.GetSection("ImagesFolderPath").Value, $"{photo.Url}.jpg");

                if (File.Exists(photoPath))
                {
                    byte[] bytes = File.ReadAllBytes(photoPath);
                    photoFiles.Add(new FileContentResult(bytes, "image/jpeg"));
                }

            });

            return  await Task.FromResult(photoFiles);

        }
    }
}
