using Ardalis.Specification;
using Domain.Enums;
using Maintenance.Application.ViewModel;

namespace Maintenance.Application.Services.ContactUs
{
    public class ContactUsFilterList : Specification<Maintenance.Domain.Entity.SettingEntities.ContactUs>
    {
        public ContactUsFilterList(ContactUsFilterViewModel filter)
        {
            Query.Where(p => !p.DeletedAt.HasValue);

            if (filter.FromDate.HasValue)
            {
                Query.Where(a => a.CreatedAt.Date >= filter.FromDate.Value.Date);
            }
            if (filter.ToDate.HasValue)
            {
                Query.Where(a => a.CreatedAt.Date <= filter.ToDate.Value.Date);
            }
            if (!string.IsNullOrWhiteSpace(filter.Status.ToString()))
            {
                var isRead = filter.Status == ContactUsStatusEnum.Read;
                Query.Where(a => a.IsRead == isRead);
            }
        }
    }
}