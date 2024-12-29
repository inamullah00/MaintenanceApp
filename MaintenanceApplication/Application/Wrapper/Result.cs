using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Maintenance.Application.Wrapper
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Value { get; private set; }
        public string? Error { get; private set; }
        public string? Message { get; private set; }
        public int StatusCode { get; private set; }

        // Constructor with all fields, allowing dynamic values for Message and StatusCode
        public Result(bool isSuccess, string error, T value, string message = null, int statusCode = 0)
        {
            IsSuccess = isSuccess;
            Error = error;
            Value = value;
            Message = message;
            StatusCode = statusCode;
        }

 
        public static Result<T> Success(T? value, string? message = null, int statusCode = 0)
        {
            return new Result<T>(true, null, value, message, statusCode);
        }

        public static Result<T> Success(string? message = null, int statusCode = 0)
        {
            return new Result<T>(true, null, default, message, statusCode);
        }

        public static Result<T> Failure(string error, string message = null, int statusCode = 0)
        {
            return new Result<T>(false, error, default, message, statusCode);
        }
        public static Result<T> Failure(string message = null, int statusCode = 0)
        {
            return new Result<T>(false, null, default, message, statusCode);
        }

    }

}
