using AutoMapper;
using Business.Managers.Photos.Commands.InsertUserPhotos;
using Business.Managers.Photos.Commands.SavePhotosToFolder;
using Business.Utilities.Results;
using Business.Utilities.Results.Abstract;
using Business.Utilities.Results.Concrete;
using Core.DataAccess.Concrete;
using DataAccess.Utilities.UnitOfWork;
using Entities.Concrete;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Managers.Photos.Commands.InsertPhoto
{
    public class Handler : BaseConnection, IRequestHandler<InsertPhotoCommand, IDataResult<int>>
    {
        private readonly IMapper _mapper;
        public Handler( IDbConnection connection,IMapper mapper)
        {
            Connection = connection;
            _mapper = mapper;
        }
        public async Task<IDataResult<int>> Handle(InsertPhotoCommand request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            { 
                var insertedPhotoId = await unitOfWork.DbContext.Photos.AddAsync(_mapper.Map<Photo>(request));

                if (!ResultUtil<int>.IsDataExist(insertedPhotoId))
                {
                    return new ErrorDataResult<int>();
                }
                return new SuccessDataResult<int>(insertedPhotoId);
            }

            

        }
    }
}
