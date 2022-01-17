using AutoMapper;
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

namespace Business.Managers.Albums.Commands.InsertAlbum
{
    public class Handler : BaseConnection,IRequestHandler<InsertAlbumCommand, IDataResult<int>>
    {
        private readonly IMapper _mapper;
        public Handler(IDbConnection connection, IMapper mapper)
        {
            Connection = connection;
            _mapper = mapper;
        }
        public async Task<IDataResult<int>> Handle(InsertAlbumCommand request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var insertedAlbumId = await unitOfWork.DbContext.Albums.AddAsync(_mapper.Map<Album>(request));

                if (!ResultUtil<int>.IsDataExist(insertedAlbumId))
                {
                    return new ErrorDataResult<int>();
                }

                return new SuccessDataResult<int>(insertedAlbumId);
            }
        }
    }
}
