using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Core.DataAccess.Abstract
{
    public interface IDbContext
    {
        IDbConnection DbConnection { get; }
        IDbTransaction DbTransaction { get; }

    }
}
