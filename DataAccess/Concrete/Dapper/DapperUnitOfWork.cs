using Core.DataAccess.Abstract;
using Core.DataAccess.Concrete;
using DataAccess.Concrete.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccess.Concrete.Dapper
{
    public class DapperUnitOfWork :BaseConnection, IUnitOfWork,IDisposable
    {

        public DapperUnitOfWork(IDbConnection dbConnection)
        {
            Connection = dbConnection;
        }

        public DapperDbContext DbContext => new DapperDbContext(Connection, Transaction);

        public void OpenConnection()
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
            
        }

        public void BeginTransaction()
        {
            OpenConnection();
            Transaction = Connection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void Commit()
        {
            Transaction.Commit();
        }

        public void RollBack()
        {
            Transaction.Rollback();
        }

        public void Dispose()
        {
            
        }
    }
}
