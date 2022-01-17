using Core.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Core.DataAccess.Concrete
{
    public class BaseConnection : IConnection
    {
        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set ; }
    }
}
