using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Common.Constants
{
    public static class SuccessMessages
    {
        // General success messages
        public const string RequestProcessedSuccessfully = "The request was processed successfully.";
        public const string ResourceCreatedSuccessfully = "The resource was created successfully.";
        public const string ResourceUpdatedSuccessfully = "The resource was updated successfully.";
        public const string ResourceDeletedSuccessfully = "The resource was deleted successfully.";
        public const string OperationSuccessful = "The operation has been completed successfully.";
        public const string PasswordChangedSuccessfully = "Your password has been changed successfully.";
        public const string EmailVerifiedSuccessfully = "Your email has been verified successfully.";

        // Specific to user operations
        public const string UserProfileUpdatedSuccessfully = "User profile has been updated successfully.";
        public const string UserRegisteredSuccessfully = "User has been registered successfully.";
        public const string UserLoggedInSuccessfully = "User has logged in successfully.";

        // Specific to payment or order operations
        public const string PaymentProcessedSuccessfully = "Payment has been processed successfully.";
        public const string OrderPlacedSuccessfully = "Order has been placed successfully.";
        public const string OrderConfirmedSuccessfully = "Order has been confirmed successfully.";
        public const string OrderAssignedSuccessfully = "Order has been Assigned successfully.";
        public const string OrderStatusUpdatedSuccessfully = "Order status updated successfully.";

        // Specific to the Button Actions
        public const string DeleteOperationSuccessfully = "Delete Operation has been Done successfully.";

        // Specific to notifications
        public const string NotificationSentSuccessfully = "Notification has been sent successfully.";
        public const string EmailSentSuccessfully = "Email has been sent successfully.";


    }

}
