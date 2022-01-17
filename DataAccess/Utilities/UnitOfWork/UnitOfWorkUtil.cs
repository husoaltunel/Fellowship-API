using DataAccess.Concrete.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccess.Utilities.UnitOfWork
{
    public static class UnitOfWorkUtil
    {
        public static DapperUnitOfWork GetUnitOfWork(IDbConnection connection)
        {
            return new DapperUnitOfWork(connection);
        }
    }
}
