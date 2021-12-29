using Core.DataAccess.Abstract;
using DataAccess.Concrete.Dapper.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccess.Concrete.Dapper
{
    public class DapperDbContext : IDbContext
    {
        public IDbConnection DbConnection { get; }

        public IDbTransaction DbTransaction { get; }

        public DapperDbContext(IDbConnection dbConnection,IDbTransaction dbTransaction)
        {
            DbConnection = dbConnection;
            DbTransaction = dbTransaction;
        }
       
        public UserRepository Users => new UserRepository(DbConnection,DbTransaction);
        public PhotoRepository Photos => new PhotoRepository(DbConnection,DbTransaction);
       
    }
}
