namespace Maintenance.Application.Exceptions
{
    public class CustomException : BaseException
    {
        public CustomException(string message) : base(message)
        {
            StatusCode = System.Net.HttpStatusCode.BadRequest;
        }
    }
}
