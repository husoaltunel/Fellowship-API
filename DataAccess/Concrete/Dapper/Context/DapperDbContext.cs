using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Dapper.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccess.Concrete.Dapper
{
    public class DapperDbContext : BaseConnection, IDbContext
    {

        public DapperDbContext(IDbConnection dbConnection,IDbTransaction dbTransaction)
        {
            Connection = dbConnection;
            Transaction = dbTransaction;
        }
       
        public UserRepository Users => new UserRepository(Connection, Transaction);
        public PhotoRepository Photos => new PhotoRepository(Connection, Transaction);
        public AlbumPhotoRepository AlbumPhotos => new AlbumPhotoRepository(Connection, Transaction);
        public AlbumRepository Albums => new AlbumRepository(Connection, Transaction);

    }
}
