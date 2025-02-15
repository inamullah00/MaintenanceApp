using Ardalis.Specification;
using Domain.Entity.UserEntities;
using Maintenance.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maintenance.Application.Services.Account.Specification
{
    public class UserSearchTable : Specification<ApplicationUser>
    {
        public UserSearchTable(string? dynamicOrder = "")
        {
            // sort order
            if (string.IsNullOrEmpty(dynamicOrder))
            {
                _ = Query.OrderByDescending(x => x.FullName == dynamicOrder); 
            }
            else
            {
                _ = Query.OrderBy(dynamicOrder);
            }
        }
    }
}
