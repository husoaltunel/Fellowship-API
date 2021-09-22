using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results.Concrete
{
    public class ErrorResult :  Result,IResult
    {
        public ErrorResult() : base(success:false)
        {

        }
        public ErrorResult(string message) : base(success:false,message)
        {

        }
    }
}
