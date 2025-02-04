using Ardalis.Specification;
using Maintenance.Domain.Entity.FreelancerEntites;

namespace Maintenance.Application.Services.FreelancerAuth.Specification
{

    public class FreelancerSearchSpecification : Specification<Freelancer>
    {
        public FreelancerSearchSpecification(string? keyword)
        {
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                Query.Where(f =>
                    f.FullName.Contains(keyword) ||
                    f.Email.Contains(keyword) ||
                    f.Status.ToString().Contains(keyword)
                );
            }

            Query.AsNoTracking();
        }
    }
}
