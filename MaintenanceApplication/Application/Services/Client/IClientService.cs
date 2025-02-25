using Application.Dto_s.ClientDto_s;
using Maintenance.Application.Dto_s.ClientDto_s.AddressDtos;
using Maintenance.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.Client
{
    public interface IClientService
    {

        // Adding a service post
        Task<Result<string>> AddServiceAsync(OfferedServiceRequestDto request);

        // Updating an existing service post
        Task<Result<OfferedServiceResponseDto>> UpdateServiceAsync(Guid serviceId, OfferedUpdateRequestDto updatedRequest);

        // Deleting a service post
        Task<Result<string>> DeleteServiceAsync(Guid serviceId);

        // Getting details of a specific service by ID
        Task<Result<OfferedServiceResponseDto>> GetServiceAsync(Guid serviceId);

        // Getting all services posted by the client
        Task<Result<List<OfferedServiceResponseDto>>> GetServicesAsync();



        // Location/Address Methods

        Task<Result<string>> SaveAddressAsync(ClientAddressRequestDto request);
        Task<Result<List<ClientAddressResponseDto>>> GetSavedAddressesAsync(Guid clientId);
        Task<Result<string>> DeleteAddressAsync(Guid addressId);

    }
}
