using Ardalis.Specification;
using Maintenance.Application.ViewModel;

namespace Maintenance.Application.Services.Admin.AdminServiceSpecification.Specification
{
    public class AdminServiceFilterList : Specification<Maintenance.Domain.Entity.FreelancerEntities.Service>
    {
        public AdminServiceFilterList(ServiceFilterViewModel filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                string searchTerm = filter.Name.ToLower().Trim();
                Query.Where(a => !string.IsNullOrEmpty(a.Name) && a.Name.ToLower().Trim().Contains(searchTerm));
            }
            if (filter.IsUserCreated.HasValue)
                Query.Where(s => s.IsUserCreated == filter.IsUserCreated.Value);

        }
    }

}
