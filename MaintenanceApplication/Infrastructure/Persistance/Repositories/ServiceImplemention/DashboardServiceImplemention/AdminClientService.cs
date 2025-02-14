using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Maintenance.Application.Common;
using Maintenance.Application.Common.Constants;
using Maintenance.Application.Exceptions;
using Maintenance.Application.Helper;
using Maintenance.Application.Services.Admin.AdminClientSpecification;
using Maintenance.Application.Services.Admin.AdminClientSpecification.Specification;
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.ClientEntities;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention.DashboardServiceImplemention
{
    public class AdminClientService : IAdminClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly IFileUploaderService _fileUploaderService;
        private readonly string _baseImageUrl;
        public AdminClientService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordService passwordService, IFileUploaderService fileUploaderService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordService = passwordService;
            _fileUploaderService = fileUploaderService;
            _baseImageUrl = _fileUploaderService.GetImageBaseUrl();
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
                ProfilePicture = !string.IsNullOrEmpty(client.ProfilePicture) ? _baseImageUrl + client.ProfilePicture : string.Empty,
            };
        }

        public async Task CreateClientAsync(ClientCreateViewModel model, CancellationToken cancellationToken)
        {
            var existingEmail = await _unitOfWork.AdminClientRepository.GetClientByEmailAsync(model.Email, cancellationToken);
            if (existingEmail != null) throw new CustomException($"Duplicate Email {model.Email}");

            var existingPhoneNumber = await _unitOfWork.AdminClientRepository.GetClientByPhoneNumberAsync(model.PhoneNumber, model.CountryId, cancellationToken);
            if (existingPhoneNumber != null) throw new CustomException($"Duplicate MobileNumber {model.PhoneNumber}");

            var country = await _unitOfWork.CountryRepository.GetByIdAsync(model.CountryId) ?? throw new CustomException("Country Not Found");
            var client = _mapper.Map<Client>(model);
            client.Country = country;
            client.Password = _passwordService.HashPassword(model.Password);

            if (model.ProfilePictureFile != null)
            {
                var imageFileName = await _fileUploaderService.SaveFileAsync(model.ProfilePictureFile, FileDirectoryConstants.Client);
                client.ProfilePicture = imageFileName;
            }
            var createResult = await _unitOfWork.AdminClientRepository.AddClient(client, cancellationToken);
            if (!createResult) throw new CustomException("Failed to create client.");
        }


        public async Task EditClientAsync(ClientEditViewModel model, CancellationToken cancellationToken)
        {
            var client = await _unitOfWork.AdminClientRepository.GetClientByIdAsync(model.Id, cancellationToken) ?? throw new CustomException("Client not found.");
            var country = await _unitOfWork.CountryRepository.GetByIdAsync(model.CountryId);
            var existingEmail = await _unitOfWork.AdminClientRepository.GetClientByEmailAsync(model.Email, cancellationToken);
            if (existingEmail != null && existingEmail.Id != model.Id) throw new CustomException($"Duplicate Email {model.Email}");
            var existingPhoneNumber = await _unitOfWork.AdminClientRepository.GetClientByPhoneNumberAsync(model.PhoneNumber, model.CountryId, cancellationToken);
            if (existingPhoneNumber != null && existingPhoneNumber.Id != model.Id) throw new CustomException($"Duplicate MobileNumber {model.PhoneNumber}");
            client.FullName = model.FullName;
            client.PhoneNumber = model.PhoneNumber;
            client.Email = model.Email;
            client.Address = model.Address;
            client.Country = country;

            if (model.ProfilePictureFile != null)
            {
                if (!string.IsNullOrEmpty(client.ProfilePicture))
                    _fileUploaderService.RemoveFile(client.ProfilePicture);

                var imageFileName = await _fileUploaderService.SaveFileAsync(model.ProfilePictureFile, FileDirectoryConstants.Client);
                client.ProfilePicture = imageFileName;
            }

            var updateResult = await _unitOfWork.AdminClientRepository.UpdateClient(client, cancellationToken);
            if (!updateResult) throw new CustomException("Failed to update freelancer.");
        }
    }
}
