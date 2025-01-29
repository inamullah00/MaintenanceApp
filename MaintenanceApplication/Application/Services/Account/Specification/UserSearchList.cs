using Ardalis.Specification;
using Domain.Entity.UserEntities;

namespace Maintenance.Application.Services.Account.Specification
{
    public class UserSearchList : Specification<ApplicationUser>
    {
        public UserSearchList(string? keyword = "", string? role = "", int pageNumber = 1, int pageSize = 10)
        {
            if (!string.IsNullOrWhiteSpace(keyword))
            {
              _ = Query.Where(x => x.FullName.Contains(keyword) || x.Email.Contains(keyword));
            }

            if (!string.IsNullOrWhiteSpace(role))
            {
                //Query.Where(x => x.Role .Equals(role));
            }
            _ = Query.OrderBy(x => x.FullName);
        }

        public UserSearchList(string UserId)
        {

            if (!string.IsNullOrWhiteSpace(UserId))
            {
                _ = Query.Where(x => x.Id == UserId);
            }
        }
    }
}
