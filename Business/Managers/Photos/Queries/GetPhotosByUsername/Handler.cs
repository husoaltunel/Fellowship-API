using Business.Managers.Photos.Queries.GetPhotosFromFolder;
using Business.Utilities.Results;
using Business.Utilities.Results.Abstract;
using Business.Utilities.Results.Concrete;
using Core.DataAccess.Concrete;
using DataAccess.Utilities.UnitOfWork;
using Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Managers.Photos.Queries.GetPhotosByUsername
{
    public class Handler : BaseConnection, IRequestHandler<GetPhotosByUsernameQuery,IDataResult<IEnumerable<PhotoDto>>>
    {
        private IWebHostEnvironment _env;
        private readonly IMediator _mediator;
        public Handler(IDbConnection dbConnection, IWebHostEnvironment env,IMediator mediator)
        {
            Connection = dbConnection;
            _env = env;
            _mediator = mediator;
        }
        public async Task<IDataResult<IEnumerable<PhotoDto>>> Handle(GetPhotosByUsernameQuery request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var result = await unitOfWork.DbContext.Photos.GetPhotosByUsernameAsync(request.Username);
                var photos = result.ToList();
                var photoDtos = new List<PhotoDto>();

                if (!ResultUtil<int>.IsDataExist(photos.Count()))
                {
                   return  new ErrorDataResult<IEnumerable<PhotoDto>>();
                }
                var photoFiles = await _mediator.Send(new GetPhotosFromFolderQuery(){Photos = photos});

                for (int i = 0; i < photos.Count; i++)
                {
                    photoDtos.Add(new PhotoDto(){ Id = photos[i].Id, PhotoFile = photoFiles[i] });
                }
                
                return new SuccessDataResult<IEnumerable<PhotoDto>>(photoDtos);


            }
        }
    }
}
