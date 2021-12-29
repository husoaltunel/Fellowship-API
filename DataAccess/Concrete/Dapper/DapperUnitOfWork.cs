using Core.DataAccess.Abstract;
using DataAccess.Concrete.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccess.Concrete.Dapper
{
    public class DapperUnitOfWork : IUnitOfWork,IDisposable
    {      
        public DapperDbContext DbContext { get;}
        public IDbConnection DbConnection { get; }

        public IDbTransaction DbTransaction { get; }

        public DapperUnitOfWork(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
            DbContext = new DapperDbContext(DbConnection,DbTransaction);
        }

        public void Dispose()
        {
            
        }
    }
}
