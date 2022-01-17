using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Core.DataAccess.Abstract
{
    public interface IConnection
    {
         IDbConnection Connection { get; set; }
        IDbTransaction Transaction { get;set;}
    }
}
