using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Common.Constants
{
    public static class HttpResponseCodes
    {
        // Success Codes (2xx)
        public const int OK = 200; 
        public const int Created = 201; 
        public const int Accepted = 202; 
        public const int NoContent = 204; // The server successfully processed the request, but there is no content to return.

        public const int BadRequest = 400; // The request cannot be fulfilled due to bad syntax.
        public const int Unauthorized = 401; // The request requires user authentication.
        public const int Forbidden = 403; // The server understood the request, but it refuses to authorize it.
        public const int NotFound = 404; // The requested resource could not be found.
        public const int MethodNotAllowed = 405; // The method specified in the request is not allowed for the resource.
        public const int Conflict = 409; // The request could not be completed due to a conflict with the current state of the resource.
        public const int PayloadTooLarge = 413; // The request is larger than the server is willing or able to process.
        public const int URITooLong = 414; // The URI provided was too long for the server to process.
        public const int UnsupportedMediaType = 415; // The server does not support the media type of the request.
        public const int UnprocessableEntity = 422; // The server understands the request but was unable to process the contained instructions.
        public const int TooManyRequests = 429; // The user has sent too many requests in a given amount of time ("rate limiting").
        public const int RequestHeaderFieldsTooLarge = 431; // The server is unwilling to process the request because its header fields are too large.

        public const int InternalServerError = 431; // The server is unwilling to process the request because its header fields are too large.

    }
}
