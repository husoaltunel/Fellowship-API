using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Utilities.Results.Abstract
{
    public interface IResult
    {
        bool Success { get;set;}
        string Message { get;set;}
    }
}
