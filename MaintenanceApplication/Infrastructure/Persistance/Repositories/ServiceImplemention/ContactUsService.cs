using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Maintenance.Application.Services.ContactUs;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention
{
    public class ContactUsService : IContactUsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public ContactUsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
    }
}
