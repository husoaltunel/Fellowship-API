using Business.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Utilities.Results.Concrete
{
    public class Result : IResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Result(bool success)
        {
            Success = success;
        }
        public Result(bool success,string message):this(success)
        {
            Message = message;
        }
    }
}
