using Business.Managers.Photos.Queries.GetPhotosFromFolder;
using Business.Utilities.Results;
using Business.Utilities.Results.Abstract;
using Business.Utilities.Results.Concrete;
using Core.DataAccess.Concrete;
using DataAccess.Utilities.UnitOfWork;
using Entities.Concrete;
using Entities.Dtos;
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

namespace Business.Managers.Photos.Queries.GetProfilePhotoByUsername
{
    public class Handler :BaseConnection, IRequestHandler<GetProfilePhotoByUsernameQuery, IDataResult<PhotoDto>>
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        public Handler(IDbConnection dbConnection, IConfiguration configuration, IMediator mediator)
        {
            Connection = dbConnection;
            _configuration = configuration;
            _mediator = mediator;
        }
        public async Task<IDataResult<PhotoDto>> Handle(GetProfilePhotoByUsernameQuery request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var result = await unitOfWork.DbContext.Photos.GetProfilePhotoByUsernameAsync(request.Username);

                if (!ResultUtil<Photo>.IsDataExist(result))
                {
                    return new ErrorDataResult<PhotoDto>();
                }
                
                var photoPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), _configuration.GetSection("ImagesFolderPath").Value, $"{result.Url}");

                if (!File.Exists(photoPath))
                {
                    return new ErrorDataResult<PhotoDto>();               
                }

                var photoList = new List<Photo>();
                photoList.Add(result);
                var photoFile = await _mediator.Send(new GetPhotosFromFolderQuery(){ Photos = photoList });
                PhotoDto photoDto = new PhotoDto() { Id = result.Id , PhotoFile = photoFile.First() };

                return new SuccessDataResult<PhotoDto>(photoDto);

            }
        }
    }
}
