using Business.Concrete;
using Business.Managers.Photo.Queries.GetPhotosFromFolder;
using Core.Utilities.Results;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Utilities.UnitOfWork;
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

namespace Business.Managers.Photo.Queries.GetPhotosByUsername
{
    public class Handler : BaseConnection, IRequestHandler<GetPhotosByUsernameQuery,IDataResult<IEnumerable<FileContentResult>>>
    {
        private IWebHostEnvironment _env;
        private readonly IMediator _mediator;
        public Handler(IDbConnection dbConnection, IWebHostEnvironment env,IMediator mediator)
        {
            Connection = dbConnection;
            _env = env;
            _mediator = mediator;
        }
        public async Task<IDataResult<IEnumerable<FileContentResult>>> Handle(GetPhotosByUsernameQuery request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var result = await unitOfWork.DbContext.Photos.GetPhotosByUsernameAsync(request.Username);
                var photos = result.ToList();
                

                if (!ResultUtil<int>.IsDataExist(result.Count()))
                {
                   return  new ErrorDataResult<IEnumerable<FileContentResult>>();
                }
                var photoFiles = await _mediator.Send(new GetPhotosFromFolderQuery(){Photos = photos});
                
                return new SuccessDataResult<IEnumerable<FileContentResult>>(photoFiles);


            }
        }
    }
}
