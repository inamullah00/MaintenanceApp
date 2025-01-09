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

     

        // Specific to the Button Actions
        public const string DeleteOperationSuccessfully = "Delete Operation has been Done successfully.";

        // Specific to notifications
        public const string NotificationSentSuccessfully = "Notification has been sent successfully.";
        public const string EmailSentSuccessfully = "Email has been sent successfully.";


        // Category Success Messages
        public const string CategoryCreated = "Category created successfully.";
        public const string CategoryUpdated = "Category updated successfully.";
        public const string CategoryDeleted = "Category deleted successfully.";
        public const string CategoryFetched = "Category fetched successfully.";
        public const string CategoriesFetched = "Categories fetched successfully.";


        #region Service Messages

        public const string ServiceCreated = "Service has been created successfully.";
        public const string ServiceUpdated = "Service has been updated successfully.";
        public const string ServiceDeleted= "Service has been deleted successfully.";
        public const string ServiceFetched = "Service found successfully.";
        public const string ServiceStatusUpdated = "Service status has been updated successfully.";
        public const string ServiceActivated= "Service has been activated successfully.";
        public const string ServiceDeactivated = "Service has been deactivated successfully.";

        #endregion

        #region Freelancer Bid Success Messages
        // General Freelancer Bid Success
        public const string FreelancerBidCreated = "The freelancer bid has been placed successfully.";
        public const string FreelancerBidUpdated = "The freelancer bid has been updated successfully.";
        public const string FreelancerBidAccepted = "The freelancer bid has been accepted successfully.";
        public const string FreelancerBidAssigned = "The freelancer bid has been assigned to the freelancer successfully.";
        public const string FreelancerBidDeleted = "The freelancer bid has been deleted successfully.";
        public const string FreelancerBidClosed = "The freelancer bid has been closed successfully.";
        public const string FreelancerBidFetched = "The freelancer bid has been fetched successfully.";

        // Freelancer Bid Status Success
        public const string FreelancerBidMarkedAsCompleted = "The freelancer bid has been marked as completed successfully.";
        public const string FreelancerBidApproved = "The freelancer bid has been approved successfully.";
        public const string FreelancerBidReopened = "The freelancer bid has been reopened successfully.";
        #endregion



        #region Order Success Messages
        // General Order Success
        public const string OrderCreatedSuccessfully = "The order has been placed successfully.";
        public const string OrderUpdatedSuccessfully = "The order has been updated successfully.";
        public const string OrderAssignedSuccessfully = "The order has been assigned to the freelancer successfully.";
        public const string OrderStatusUpdatedSuccessfully = "The order status has been updated successfully.";
        public const string OrderCompletedSuccessfully = "The order has been completed successfully.";
        public const string OrderClosedSuccessfully = "The order has been closed successfully.";
        public const string OrderDeletedSuccessfully = "The order has been deleted successfully.";
        public const string OrderFetchedSuccessfully = "The orders details have been fetched successfully.";

        // Order Status Success
        public const string OrderMarkedAsCompletedSuccessfully = "The order has been marked as completed successfully.";
        public const string OrderReopenedSuccessfully = "The order has been reopened successfully.";
        public const string OrderResolvedSuccessfully = "The order has been resolved successfully.";
        public const string OrderConfirmedSuccessfully = "The order has been confirmed successfully.";
        public const string OrderAssignedToFreelancerSuccessfully = "The order has been assigned to the freelancer successfully.";
        #endregion


    }

}
