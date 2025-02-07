using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Maintenance.Application.Exceptions;
using Maintenance.Application.Helper;
using Maintenance.Application.Services.Admin.AdminServiceSpecification;
using Maintenance.Application.Services.Admin.AdminServiceSpecification.Specification;
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.FreelancerEntities;
using Maintenance.Infrastructure.Extensions;

namespace Maintenance.Application.Services
{
    public class AdminSevService : IAdminSevService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminSevService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedResponse<ServiceResponseViewModel>> GetFilteredServicesAsync(ServiceFilterViewModel filter)
        {
            var specification = new AdminServiceFilterList(filter);
            return await _unitOfWork.AdminServiceRepository.GetFilteredServiceAsync(filter, specification);
        }

        public async Task<ServiceEditViewModel> GetServiceForEditAsync(Guid id)
        {
            var service = await _unitOfWork.AdminServiceRepository.GetServiceByIdAsync(id) ?? throw new CustomException("Service not found.");
            return new ServiceEditViewModel
            {
                Id = service.Id,
                Name = service.Name,
                IsUserCreated = service.IsUserCreated,
                IsApproved = service.IsApproved,
            };

        }

        public async Task AddServiceAsync(ServiceCreateViewModel model)
        {
            var adminId = AppHttpContext.GetAdminCurrentUserId();
            var user = await _unitOfWork.AdminServiceRepository.GetAdminByIdAsync(adminId) ?? throw new CustomException("User Not Found.");
            model.Name = NormalizeNamesHelper.NormalizeNames(model.Name);
            bool serviceExists = await _unitOfWork.AdminServiceRepository.ServiceExistsAsync(model.Name);
            if (serviceExists) throw new CustomException("A service with this name already exists.");

            var service = new Service
            {
                Name = model.Name,
                IsApproved = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            };
            service.MarkAsSystemCreated(user);

            await _unitOfWork.AdminServiceRepository.AddServiceAsync(service);
        }

        public async Task UpdateServiceAsync(ServiceEditViewModel model)
        {
            var adminId = AppHttpContext.GetAdminCurrentUserId();
            var service = await _unitOfWork.AdminServiceRepository.GetServiceByIdAsync(model.Id) ?? throw new CustomException("Service not found");
            var user = await _unitOfWork.AdminServiceRepository.GetAdminByIdAsync(adminId) ?? throw new CustomException("User Not Found.");
            model.Name = NormalizeNamesHelper.NormalizeNames(model.Name);
            bool serviceExists = await _unitOfWork.AdminServiceRepository.ServiceExistsAsync(model.Name);
            if (serviceExists) throw new CustomException("A service with this name already exists.");

            service.Name = model.Name;
            service.UpdatedAt = DateTime.UtcNow;
            service.MarkAsApproved();
            service.Activate();
            service.MarkAsSystemCreated(user);
            var UpdatedResult = await _unitOfWork.AdminServiceRepository.UpdateServiceAsync(service);
            if (!UpdatedResult) throw new CustomException("Failed to update service.");
        }

        public async Task ApproveServiceAsync(Guid serviceId)
        {
            var service = await _unitOfWork.AdminServiceRepository.GetServiceByIdAsync(serviceId) ?? throw new CustomException("Service not found");
            service.MarkAsApproved();
            service.Activate();
            var UpdatedResult = await _unitOfWork.AdminServiceRepository.UpdateServiceAsync(service);
            if (!UpdatedResult) throw new CustomException("Failed to approve service.");
        }

        public async Task ActivateServiceAsync(Guid serviceId)
        {
            var service = await _unitOfWork.AdminServiceRepository.GetServiceByIdAsync(serviceId) ?? throw new CustomException("Service not found");
            service.Activate();
            var UpdatedResult = await _unitOfWork.AdminServiceRepository.UpdateServiceAsync(service);
            if (!UpdatedResult) throw new CustomException("Failed to activate service.");
        }

        public async Task DeactivateServiceAsync(Guid serviceId)
        {
            var service = await _unitOfWork.AdminServiceRepository.GetServiceByIdAsync(serviceId) ?? throw new CustomException("Service not found");
            service.Deactivate();
            var UpdatedResult = await _unitOfWork.AdminServiceRepository.UpdateServiceAsync(service);
            if (!UpdatedResult) throw new CustomException("Failed to deactivate service.");
        }


    }
}
