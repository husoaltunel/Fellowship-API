using Core.DataAccess.Concrete.Dapper;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccess.Concrete.Dapper.Repositories
{
    public class AlbumPhotoRepository : DpBaseRepository<AlbumPhoto> , IAlbumPhotoRepository
    {
        public AlbumPhotoRepository(IDbConnection connection,IDbTransaction transaction):base(connection,transaction)
        {

        }
    }
}
