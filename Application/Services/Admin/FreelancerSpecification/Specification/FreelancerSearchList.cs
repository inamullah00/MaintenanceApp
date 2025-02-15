using Ardalis.Specification;
using Maintenance.Application.ViewModel;
using Maintenance.Domain.Entity.FreelancerEntites;

namespace Maintenance.Application.Services.Admin.FreelancerSpecification.Specification
{
    public class FreelancerSearchList : Specification<Freelancer>
    {
        public FreelancerSearchList(FreelancerFilterViewModel filter)
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

            if (filter.AccountStatus.HasValue)
            {
                Query.Where(f => f.Status == filter.AccountStatus.Value);
            }
        }

    }

}
