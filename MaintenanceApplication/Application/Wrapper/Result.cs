using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Wrapper
{
    public class Result
    {
        public bool IsSuccess { get; }
        public Error Error { get; }

        public Result(bool isSuccess, Error error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success()
        {
            return new Result(true, Error.None);
        }

        public static Result Failure(Error error)
        {
            return new Result(false, error);
        }

        public static Result<T> Success<T>(T data)
        {
            return new Result<T>(true, Error.None, data);
        }

        public static Result<T> Failure<T>(Error error)
        {
            return new Result<T>(false, error, default);
        }
    }

}
