using Application.Dto_s.ClientDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ServiceInterfaces.ClientInterfaces
{
    public interface IClientService
    {

        // Adding a service post
        Task<(bool Success, string Message)> AddServiceAsync(OfferedServiceRequestDto request);

        // Updating an existing service post
        Task<(bool Success, string Message)> UpdateServiceAsync(Guid serviceId, OfferedUpdateRequestDto updatedRequest);

        // Deleting a service post
        Task<(bool Success, string Message)> DeleteServiceAsync(Guid serviceId);

        // Getting details of a specific service by ID
        Task<(bool Success, OfferedServiceResponseDto? Service, string Message)> GetServiceAsync(Guid serviceId);

        // Getting all services posted by the client
        Task<(bool Success, List<OfferedServiceResponseDto>? Services, string Message)> GetServicesAsync();

        // Getting bids submitted by freelancers for a specific service
        //Task<(bool Success, List<BidResponseDto>? Bids, string Message)> GetServiceBidsAsync(int serviceId);

        // Accepting a freelancer's bid for a specific service
        //Task<(bool Success, string Message)> AcceptBidAsync(int serviceId, int freelancerId);

        // Cancelling a posted service (e.g., if no suitable bids are received)
        //Task<(bool Success, string Message)> CancelServiceAsync(int serviceId);

        // Rating a freelancer after service completion
        //Task<(bool Success, string Message)> RateFreelancerAsync(int serviceId, FreelancerRatingDto rating);

        // Resolving disputes or reporting issues related to a service
        //Task<(bool Success, string Message)> ReportIssueAsync(int serviceId, string issueDescription);

    }
}
