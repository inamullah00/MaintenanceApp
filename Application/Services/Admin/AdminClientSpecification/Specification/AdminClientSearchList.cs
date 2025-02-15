using Ardalis.Specification;
using Maintenance.Application.ViewModel;

namespace Maintenance.Application.Services.Admin.AdminClientSpecification.Specification
{
    public class AdminClientSearchList : Specification<Maintenance.Domain.Entity.ClientEntities.Client>
    {
        public AdminClientSearchList(ClientFilterViewModel filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.FullName))
            {
                string searchTerm = filter.FullName.ToLower().Trim();

                Query.Where(a =>
                    (!string.IsNullOrEmpty(a.FullName) && a.FullName.ToLower().Trim().Contains(searchTerm)) ||
                    (!string.IsNullOrEmpty(a.Email) && a.Email.ToLower().Trim().Equals(searchTerm)) ||
                    (!string.IsNullOrEmpty(a.PhoneNumber) && a.PhoneNumber.ToLower().Trim().Equals(searchTerm))
                );
            }

        }

    }

}
