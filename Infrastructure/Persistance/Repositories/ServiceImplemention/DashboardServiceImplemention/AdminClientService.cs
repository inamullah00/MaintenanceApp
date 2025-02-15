using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Maintenance.Application.Exceptions;
using Maintenance.Application.Services.Admin.AdminClientSpecification;
using Maintenance.Application.Services.Admin.AdminClientSpecification.Specification;
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention.DashboardServiceImplemention
{
    public class AdminClientService : IAdminClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminClientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedResponse<ClientResponseViewModel>> GetFilteredClientsAsync(ClientFilterViewModel filter)
        {
            var specification = new AdminClientSearchList(filter);
            return await _unitOfWork.AdminClientRepository.GetFilteredClientAsync(filter, specification);
        }

        public async Task<ClientEditViewModel> GetClientForEditAsync(Guid id, CancellationToken cancellationToken)
        {
            var client = await _unitOfWork.AdminClientRepository.GetClientByIdAsync(id, cancellationToken) ?? throw new CustomException("Client not found.");
            return new ClientEditViewModel
            {
                Id = client.Id,
                FullName = client.FullName,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                CountryId = client.CountryId,
            };
        }
        public async Task EditClientAsync(ClientEditViewModel model, CancellationToken cancellationToken)
        {
            var freelancer = await _unitOfWork.AdminClientRepository.GetClientByIdAsync(model.Id, cancellationToken) ?? throw new CustomException("Client not found.");
            var country = await _unitOfWork.CountryRepository.GetByIdAsync(model.CountryId);
            var existingEmail = await _unitOfWork.AdminClientRepository.GetClientByEmailAsync(model.Email, cancellationToken);
            if (existingEmail != null && existingEmail.Id != model.Id) throw new CustomException($"Duplicate Email {model.Email}");
            var existingPhoneNumber = await _unitOfWork.AdminClientRepository.GetClientByPhoneNumberAsync(model.PhoneNumber, model.CountryId, cancellationToken);
            if (existingPhoneNumber != null && existingPhoneNumber.Id != model.Id) throw new CustomException($"Duplicate MobileNumber {model.PhoneNumber}");
            _mapper.Map(model, freelancer);
            freelancer.Country = country;

            var updateResult = await _unitOfWork.AdminClientRepository.UpdateClient(freelancer, cancellationToken);
            if (!updateResult) throw new CustomException("Failed to update freelancer.");
        }
    }
}
