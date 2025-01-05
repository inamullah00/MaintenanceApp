using Ardalis.Specification;
using Domain.Entity.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.Account.Specification
{
    public class UserSearchList : Specification<ApplicationUser>
    {
        public UserSearchList(string? keyword = "", string? role = "" )
        {
            if (!string.IsNullOrWhiteSpace(keyword))
            {
              _ = Query.Where(x => x.FirstName.Contains(keyword) ||
                                 x.LastName.Contains(keyword) ||
                                 x.Email.Contains(keyword));
            }

            if (!string.IsNullOrWhiteSpace(role))
            {
                //Query.Where(x => x.Role .Equals(role));
            }
            _ = Query.OrderBy(x => x.FirstName);
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
