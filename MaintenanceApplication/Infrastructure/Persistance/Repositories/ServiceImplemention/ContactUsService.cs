using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Maintenance.Application.Exceptions;
using Maintenance.Application.Services.ContactUs;
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;
using Maintenance.Infrastructure.Extensions;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention
{
    public class ContactUsService : IContactUsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactUsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResponse<ContactUsResponseViewModel>> GetAllListAsync(ContactUsFilterViewModel filter)
        {
            var specification = new ContactUsFilterList(filter);
            return await _unitOfWork.ContactUsRepository.GetAllListAsync(filter, specification);
        }

        public async Task<PaginatedResponse<ContactUsResponseViewModel>> GetPagedListAsync(ContactUsFilterViewModel filter)
        {
            return await _unitOfWork.ContactUsRepository.GetPagedListAsync(filter);
        }
        public async Task<int> NotificationCountAsync()
        {
            return await _unitOfWork.ContactUsRepository.NotificationCount().ConfigureAwait(false);
        }
        public async Task<int> MarkAsRead(Guid id, CancellationToken cancellationToken)
        {
            var contactUs = await _unitOfWork.ContactUsRepository.GetContactUsByIdAsync(id, cancellationToken) ?? throw new CustomException("ContactUs user not found");
            var adminId = AppHttpContext.GetAdminCurrentUserId();
            var user = await _unitOfWork.AdminServiceRepository.GetAdminByIdAsync(adminId, cancellationToken) ?? throw new CustomException("User Not Found.");

            contactUs.MarkAsRead(user);

            var UpdatedResult = await _unitOfWork.ContactUsRepository.UpdateContactUs(contactUs, cancellationToken);
            if (!UpdatedResult) throw new CustomException("Failed to update contact us.");

            return await NotificationCountAsync();
        }

    }
}
