using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results.Concrete
{
    public class ErrorDataResult<TEntity> : DataResult<TEntity>, IDataResult<TEntity>
    {
        public ErrorDataResult() : base(success: false)
        {

        }
        public ErrorDataResult(string message) : base(success: false, message)
        {

        }
        public ErrorDataResult(TEntity data) : base(data, success: false)
        {

        }
        public ErrorDataResult(TEntity data, string message) : base(data, success: false, message)
        {

        }
    }
}
