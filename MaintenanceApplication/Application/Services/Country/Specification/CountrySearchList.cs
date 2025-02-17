using Ardalis.Specification;
using Maintenance.Application.ViewModel;
using Maintenance.Domain.Entity.FreelancerEntities;

namespace Maintenance.Application.Services
{
    public class CountrySearchList : Specification<Country>
    {

        public CountrySearchList(CountryFilterViewModel filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                string searchTerm = filter.Name.ToLower().Trim();

                Query.Where(a =>
                    (!string.IsNullOrEmpty(a.Name) && a.Name.ToLower().Trim().Contains(searchTerm)) ||
                    (!string.IsNullOrEmpty(a.DialCode) && a.DialCode.ToLower().Trim().Equals(searchTerm))
                );
            }
        }
    }
}
