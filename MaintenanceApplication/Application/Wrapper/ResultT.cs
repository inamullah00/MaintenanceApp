using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Wrapper
{
    public class Result<T> : Result
    {
        public T Data { get; }

        // Constructor for the generic Result<T> class
        public Result(bool isSuccess, Error error, T data) : base(isSuccess, error)
        {
            Data = data;
        }

        // Static methods to handle success/failure with data
        public static Result<T> Success(T data)
        {
            return new Result<T>(true, Error.None, data);
        }

        public static Result<T> Failure(Error error)
        {
            return new Result<T>(false, error, default);
        }
    }
}
