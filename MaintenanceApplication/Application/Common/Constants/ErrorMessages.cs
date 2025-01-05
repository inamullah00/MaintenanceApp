using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Common.Constants
{
        public static class ErrorMessages
        {
            // User-related error messages
            public const string UserNotFound = "User not found with the provided ID";
            public const string UserNotAuthorized = "User is not authorized to perform this action";
            public const string EmailAlreadyInUse = "The provided email is already in use by another user";
            public const string UsernameAlreadyInUse = "The provided username is already in use";
            public const string InvalidPassword = "The provided password is incorrect";
            public const string PasswordTooWeak = "The provided password does not meet the required strength criteria";
            public const string UserLockedOut = "The user account is locked out due to multiple failed login attempts";
            public const string UserBanned = "The user account has been banned";
            public const string UserInactive = "The user account is inactive";

            // Authentication & Authorization-related error messages
            public const string UnauthorizedAccess = "You are not authorized to perform this action";
            public const string InvalidToken = "The provided authentication token is invalid or expired";
            public const string InvalidCredentials = "The username or password is incorrect";
            public const string TokenRefreshFailed = "Failed to refresh the authentication token";
            public const string InvalidApiKey = "The provided API key is invalid or missing";

            // Data-related error messages
            public const string DatabaseError = "An error occurred while saving to the database";
            public const string DataNotFound = "No data found matching the provided criteria";
            public const string ResourceNotFound = "No record found with the provided ID.";
            public const string DataConflict = "There is a conflict with the data, unable to proceed";
            public const string DataIntegrityViolation = "Data integrity violation occurred";
            public const string ValidationError = "The provided data is not valid";
            public const string InvalidOrEmptyId = "The provided ID is either invalid or missing.";
            public const string InvalidOrEmpty = "The provided Inputs is either invalid or missing.";


        // System errors
        public const string InternalServerError = "An error occurred while processing the request";
            public const string ServiceUnavailable = "The service is currently unavailable, please try again later";
            public const string UnknownError = "An unknown error occurred";
            public const string NetworkError = "A network error occurred, please check your connection";
            public const string TimeoutError = "The request timed out, please try again later";
            public const string DeletingResourceError = "An Error Occurred While Deleting the Resource";


            // Input-related error messages
            public const string MissingRequiredField = "One or more required fields are missing";
            public const string InvalidInputFormat = "The input format is incorrect";
            public const string InvalidFieldValue = "The provided value for the field is invalid";

            // Action or Operation errors
            public const string OperationNotAllowed = "The requested operation is not allowed";
            public const string OperationFailed = "The operation failed due to an unexpected error";
            public const string OperationSuccess = "The operation has been Success";
            public const string ActionNotSupported = "The requested action is not supported by the system";

            // Resource-related error messages
            public const string ResourceConflict = "The resource is in conflict with an existing one";
            public const string ResourceAlreadyExists = "The resource already exists";

            // Payment/Transaction error messages
            public const string PaymentFailed = "Payment processing failed, please try again";
            public const string PaymentDeclined = "Your payment was declined by the payment provider";
            public const string InsufficientFunds = "Insufficient funds to complete the transaction";

            // File upload error messages
            public const string FileTooLarge = "The uploaded file exceeds the maximum allowed size";
            public const string UnsupportedFileType = "The uploaded file type is not supported";
            public const string FileUploadError = "An error occurred while uploading the file";

            // General operation error messages
            public const string ActionFailed = "The action could not be completed, please try again";
            public const string RequestFailed = "The request could not be processed at this time";
        }
}
