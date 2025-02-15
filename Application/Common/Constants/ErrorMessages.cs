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
            public const string EmailAlreadyExists = "The provided email is already in use by another user";
            public const string UsernameAlreadyInUse = "The provided username is already in use";
            public const string InvalidPassword = "The provided password is incorrect";
            public const string PasswordTooWeak = "The provided password does not meet the required strength criteria";
            public const string UserLockedOut = "The user account is locked out due to multiple failed login attempts";
            public const string UserBanned = "The user account has been banned";
            public const string UserInactive = "The user account is inactive";
            public const string FreelancerRegistrationFailed = "Failled To Register!";
            public const string AlreadyApproved = "The Specified Account is Already Approved!";
            public const string ApprovalFailed = "Failed To Approve!. Please try again Later";
            public const string UpdateFailed = "Failed To Update User Profile!. Please try again Later";


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
        public const string InternalServerError = "An unexpected error occurred. Please try again later.";
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

        #region Category Messages
        // General Category Errors
        public const string CategoryNotFound = "No category found with the provided ID.";
        public const string CategoryAlreadyExists = "Category already exists.";
        public const string CategoryCreationFailed = "An error occurred while adding the category.";
        public const string CategoryUpdateFailed = "An error occurred while update the category.";
        public const string CategoryDeletionFailed = "Failed To Delete Category.It may not exist or any other Server error";
        public const string InvalidCategoryId = "The provided  category ID is invalid or Empty.";
        public const string InvalidCategoryData = "The provided category data is invalid.";

        // Category Status Errors
        public const string CategoryInactive = "The category is inactive.";
        public const string CategoryAlreadyInactive = "The category is already inactive.";
        public const string CategoryAlreadyActive = "The category is already active.";

        // Category Validation Errors
        public const string CategoryNameRequired = "Category name is required.";
        public const string CategoryNameTooShort = "Category name is too short.";
        public const string CategoryNameTooLong = "Category name is too long.";
        public const string InvalidCategoryType = "The provided category type is invalid.";

        #endregion


        #region Service Messages
        // General Service Errors
        public const string ServiceNotFound = "No service found with the provided ID.";
        public const string ServiceAlreadyExists = "Service already exists.";
        public const string ServiceCreationFailed = "An error occurred while adding the service.";
        public const string ServiceUpdateFailed = "An error occurred while updating the service.";
        public const string ServiceDeletionFailed = "Failed to delete service. It may not exist or another server error occurred.";
        public const string InvalidServiceId = "The provided service ID is invalid or empty.";
        public const string InvalidServiceData = "The provided service data is invalid.";

        // Service Status Errors
        public const string ServiceInactive = "The service is inactive.";
        public const string ServiceAlreadyInactive = "The service is already inactive.";
        public const string ServiceAlreadyActive = "The service is already active.";
        #endregion


        #region Freelancer Bid Messages
        // General Freelancer Bid Errors
        public const string BidNotFound = "No bid found with the provided ID.";
        public const string FreelancerBidAlreadyExists = "A bid from the freelancer already exists for this job.";
        public const string FreelancerBidCreationFailed = "An error occurred while Submiting the bid.Please Try again later";
        public const string FreelancerBidApprovalFailed = "An error occurred while Approve the bid.Please Try again later";
        public const string FreelancerBidUpdateFailed = "An error occurred while updating the bid.";
        public const string FreelancerBidDeletionFailed = "Failed to delete the bid. It may not exist or another server error occurred.";
        public const string FreelancerBidAlreadyAccepted = "The bid has already been accepted.";
        public const string FreelancerBidExpired = "The bid has expired and can no longer be modified.";
        public const string InvalidFreelancerId = "The provided freelancer ID Aagainst Bid is invalid or empty.";
        public const string InvalidFreelancerBidId = "The provided Bid ID is Invalid or empty.";
        public const string InvalidFreelancerBidData = "The provided bid data is invalid.";
        public const string FreelancerBidAlreadyAssigned = "The bid has already been assigned to another freelancer.";
        public const string FreelancerBidNotFound = "No bid found for the provided freelancer ID.";

        // Freelancer Bid Validation Errors
        public const string FreelancerBidAmountTooLow = "The bid amount is too low for this job.";
        public const string FreelancerBidAmountTooHigh = "The bid amount is too high for this job.";
        public const string FreelancerBidDescriptionRequired = "The bid description is required.";
        public const string FreelancerBidDescriptionTooShort = "The bid description is too short.";
        public const string FreelancerBidDescriptionTooLong = "The bid description is too long.";

        // Freelancer Bid Status Errors
        public const string FreelancerBidNotInPendingStatus = "The bid is not in a pending status and cannot be updated.";
        public const string FreelancerBidAlreadyClosed = "The bid has already been closed and cannot be modified.";
        #endregion

        #region Order Messages
        // General Order Errors
        public const string OrderNotFound = "No order found with the provided ID.";
        public const string OrderAlreadyExists = "An order with the provided details already exists.";
        public const string OrderCreationFailed = "An error occurred while placing the order. Please try again later.";
        public const string OrderUpdateFailed = "An error occurred while updating the order. Please try again later.";
        public const string OrderDeletionFailed = "Failed to delete the order. It may not exist or another server error occurred.";
        public const string OrderAlreadyAssigned = "The order has already been assigned to a freelancer.";
        public const string OrderAlreadyCompleted = "The order has already been completed and cannot be modified.";
        public const string InvalidOrderId = "The provided order ID is invalid or empty.";
        public const string InvalidOrderData = "The provided order data is invalid.";
        public const string OrderNotAssigned = "The order has not been assigned to any freelancer.";
        public const string OrderStatusUpdateFailed = "Failed to update the status of the order.";
        public const string OrderNotFoundForFreelancer = "No order found for the provided freelancer ID.";
        public const string OrderAssignmentFailed = "Cannot assign this order.The order is not in a pending state.";

        // Order Validation Errors
        public const string OrderAmountTooLow = "The order amount is too low.";
        public const string OrderAmountTooHigh = "The order amount is too high.";
        public const string OrderDescriptionRequired = "The order description is required.";
        public const string OrderDescriptionTooShort = "The order description is too short.";
        public const string OrderDescriptionTooLong = "The order description is too long.";
        public const string OrderBudgetRequired = "The order budget is required.";
        public const string InvalidOrderBudget = "The provided order budget is invalid.";

        // Order Status Errors
        public const string OrderNotInPendingStatus = "The order is not in a pending status and cannot be updated.";
        public const string OrderAlreadyClosed = "The order has already been closed and cannot be modified.";
        public const string OrderAlreadyInProgress = "The order is already in progress and cannot be assigned.";
        public const string OrderAlreadyResolved = "The order has already been resolved and cannot be modified.";


        public const string NoInProcessOrdersFound = "No Any InProcess Orders Found!.";
        public const string NoInProgressOrdersFound = "No Any InProgress Orders Found!.";
        public const string NoAnyCompletedOrdersFound = "No Any Completed Orders Found!.";
        #endregion

        #region Feedback Error Messages

        public const string FeedbackCreationFailed = "Failed to create feedback.";
        public const string FeedbackUpdateFailed = "Failed to update feedback.";
        public const string FeedbackDeletionFailed = "Failed to delete feedback.";
        public const string FeedbackNotFound = "Feedback not found.";
        public const string FeedbackInvalidOrEmpty = "Invalid or empty feedback data.";
        public const string FeedbackStatusUpdateFailed = "Failed to update feedback status.";
        public const string FeedbackActivationFailed = "Failed to activate feedback.";
        public const string FeedbackDeactivationFailed = "Failed to deactivate feedback.";

        #endregion


        // Freelancer as a Usre
        public const string FreelancerNotFound = "Freelancer not found with the provided ID";

        // Package Messages

        public const string PackageCreationFailed = "Failed To Create Package. Please try again ";

        // Client 

        public const string ClientNotFound = "Record Not Found For the Specified Client Id";
    }
}
