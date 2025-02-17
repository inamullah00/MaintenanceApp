using Ardalis.Specification;
using Maintenance.Application.ViewModel;
using Maintenance.Domain.Entity.FreelancerEntities;

namespace Maintenance.Application.Services.Admin.AdminServiceSpecification.Specification
{
    public class AdminPackageFilterList : Specification<Package>
    {
        public AdminPackageFilterList(PackageFilterViewModel filter)
        {
            Query.Where(p => !p.DeletedAt.HasValue);

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                string searchTerm = filter.Name.ToLower().Trim();
                Query.Where(a => !string.IsNullOrEmpty(a.Name) && a.Name.ToLower().Trim().Contains(searchTerm));
            }
            if (!string.IsNullOrEmpty(filter.FreelancerId))
            {
                Query.Where(x => x.Id.ToString() == filter.FreelancerId);
            }

        }
    }

}
