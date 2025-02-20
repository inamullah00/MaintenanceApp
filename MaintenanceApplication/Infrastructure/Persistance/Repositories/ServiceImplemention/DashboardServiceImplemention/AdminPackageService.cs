using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Maintenance.Application.Exceptions;
using Maintenance.Application.Services.Admin.AdminPackageSpecification;
using Maintenance.Application.Services.Admin.AdminServiceSpecification.Specification;
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.FreelancerEntities;
using Maintenance.Infrastructure.Extensions;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention.DashboardServiceImplemention
{
    public class AdminPackageService : IAdminPackageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminPackageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResponse<PackageResponseViewModel>> GetFilteredPackagesAsync(PackageFilterViewModel filter)
        {
            var specification = new AdminPackageFilterList(filter);
            return await _unitOfWork.AdminPackageRepository.GetFilteredPackagesAsync(filter, specification);
        }

        public async Task<IList<PackageResponseViewModel>> GetAllPackagesAsync()
        {
            return await _unitOfWork.AdminPackageRepository.GetAllAsync();
        }

        public async Task<PackageEditViewModel> GetPackageForEditAsync(Guid id, CancellationToken cancellationToken)
        {
            var package = await _unitOfWork.AdminPackageRepository.GetPackageByIdAsync(id, cancellationToken) ?? throw new CustomException("Package not found.");
            return new PackageEditViewModel
            {
                Id = package.Id,
                Name = package.Name,
                Price = package.Price,
                OfferDetails = package.OfferDetails,
                FreelancerId = package.FreelancerId,

            };

        }

        public async Task AddPackageAsync(PackageCreateViewModel model, CancellationToken cancellationToken)
        {
            var adminId = AppHttpContext.GetAdminCurrentUserId();
            var user = await _unitOfWork.AdminServiceRepository.GetAdminByIdAsync(adminId) ?? throw new CustomException("User Not Found.");

            bool serviceExists = await _unitOfWork.AdminPackageRepository.PackageExistsAsync(model.Name);
            if (serviceExists) throw new CustomException("A package with this name already exists.");

            var package = _mapper.Map<Package>(model);
            package.SetActionBy(user);
            await _unitOfWork.AdminPackageRepository.AddPackageAsync(package, cancellationToken);
        }

        public async Task UpdatePackageAsync(PackageEditViewModel model, CancellationToken cancellationToken)
        {
            var adminId = AppHttpContext.GetAdminCurrentUserId();
            var package = await _unitOfWork.AdminPackageRepository.GetPackageByIdAsync(model.Id, cancellationToken) ?? throw new CustomException("Service not found");
            var user = await _unitOfWork.AdminServiceRepository.GetAdminByIdAsync(adminId) ?? throw new CustomException("User Not Found.");

            bool packageExists = await _unitOfWork.AdminPackageRepository.PackageExistsAsync(model.Name);
            if (packageExists && package.Id != model.Id) throw new CustomException("A package with this name already exists.");

            _mapper.Map(model, package);
            package.SetActionBy(user);

            var UpdatedResult = await _unitOfWork.AdminPackageRepository.UpdatePackageAsync(package, cancellationToken);
            if (!UpdatedResult) throw new CustomException("Failed to update package.");
        }

        public async Task DeletePackageAsync(Guid id, CancellationToken cancellationToken)
        {
            var adminId = AppHttpContext.GetAdminCurrentUserId();
            var package = await _unitOfWork.AdminPackageRepository.GetPackageByIdAsync(id, cancellationToken) ?? throw new CustomException("Service not found");
            var user = await _unitOfWork.AdminServiceRepository.GetAdminByIdAsync(adminId) ?? throw new CustomException("User Not Found.");

            package.MarkAsDeleted(user);

            var UpdatedResult = await _unitOfWork.AdminPackageRepository.UpdatePackageAsync(package, cancellationToken);
            if (!UpdatedResult) throw new CustomException("Failed to update package.");
        }
    }
}
