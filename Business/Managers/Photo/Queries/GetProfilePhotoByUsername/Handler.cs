using Business.Concrete;
using Business.Managers.Photo.Queries.GetPhotosFromFolder;
using Core.Utilities.Results;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Utilities.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Managers.Photo.Queries.GetProfilePhotoByUsername
{
    public class Handler :BaseConnection, IRequestHandler<GetProfilePhotoByUsernameQuery, IDataResult<FileContentResult>>
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        public Handler(IDbConnection dbConnection, IConfiguration configuration, IMediator mediator)
        {
            Connection = dbConnection;
            _configuration = configuration;
            _mediator = mediator;
        }
        public async Task<IDataResult<FileContentResult>> Handle(GetProfilePhotoByUsernameQuery request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var result = await unitOfWork.DbContext.Photos.GetProfilePhotoByUsernameAsync(request.Username);

                if (!ResultUtil<Entities.Concrete.Photo>.IsDataExist(result))
                {
                    return new ErrorDataResult<FileContentResult>();
                }
                
                var photoPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), _configuration.GetSection("ImagesFolderPath").Value, $"{result.Url}.jpg");

                if (!File.Exists(photoPath))
                {
                    return new ErrorDataResult<FileContentResult>();               
                }

                var photoList = new List<Entities.Concrete.Photo>();
                photoList.Add(result);
                var photoFile = await _mediator.Send(new GetPhotosFromFolderQuery(){ Photos = photoList });

                return new SuccessDataResult<FileContentResult>(photoFile.FirstOrDefault());

            }
        }
    }
}
