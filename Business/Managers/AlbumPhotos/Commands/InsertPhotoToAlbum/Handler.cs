using AutoMapper;
using Business.Utilities.Results;
using Business.Utilities.Results.Abstract;
using Business.Utilities.Results.Concrete;
using Core.DataAccess.Concrete;
using DataAccess.Utilities.UnitOfWork;
using Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Managers.AlbumPhotos.Commands.InsertPhotoToAlbum
{
    public class Handler : BaseConnection,IRequestHandler<InsertPhotoToAlbumCommand, IDataResult<int>>
    {
        private readonly IMapper _mapper;

        public Handler(IDbConnection connection, IMapper mapper)
        {
            Connection = connection;
            _mapper = mapper;
        }
        public async Task<IDataResult<int>> Handle(InsertPhotoToAlbumCommand request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var insertedAlbumPhotoId = await unitOfWork.DbContext.AlbumPhotos.AddAsync(_mapper.Map<AlbumPhoto>(request));

                if (!ResultUtil<int>.IsDataExist(insertedAlbumPhotoId))
                {
                    return new ErrorDataResult<int>();
                }

                return new SuccessDataResult<int>(insertedAlbumPhotoId);
            }
        }
    }
}
