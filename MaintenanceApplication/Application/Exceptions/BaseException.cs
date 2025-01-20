using System.Net;

namespace Maintenance.Application.Exceptions
{
    public class BaseException : Exception
    {
        public HttpStatusCode StatusCode { get; protected set; }

        public BaseException(string message) : base(message)
        {
        }
    }
}
