using Application.Dto_s.ClientDto_s;
using Maintenance.Application.Dto_s.ClientDto_s.AddressDtos;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.ClientEntities;
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
        Task<Result<string>> AddServiceAsync(OfferedServiceRequestDto request, CancellationToken cancellationToken = default);

        // Updating an existing service post
        Task<Result<OfferedServiceResponseDto>> UpdateServiceAsync(Guid serviceId, OfferedUpdateRequestDto updatedRequest, CancellationToken cancellationToken = default);

        // Deleting a service post
        Task<Result<string>> DeleteServiceAsync(Guid serviceId, CancellationToken cancellationToken = default);

        // Getting details of a specific service by ID
        Task<Result<OfferedServiceResponseDto>> GetServiceAsync(Guid serviceId, CancellationToken cancellationToken = default);

        // Getting all services posted by the client
        Task<Result<List<OfferedServiceResponseDto>>> GetServicesAsync();



        // Location/Address Methods

        Task<Result<string>> SaveAddressAsync(ClientAddressRequestDto request , CancellationToken cancellationToken =default);
        Task<Result<List<ClientAddressResponseDto>>> GetSavedAddressesAsync(Guid clientId,CancellationToken cancellationToken=default);
        Task<Result<string>> DeleteAddressAsync(Guid addressId, CancellationToken cancellationToken = default);

        Task<Result<string>> UpdateAddressAsync(Guid addressId, ClientAddressUpdateDto updateDto,CancellationToken cancellationToken = default);
    }
}
