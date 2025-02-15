using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Maintenance.Application.Common.Filters;


namespace Maintenance.Application.Services.Account.Filter
{
    public class UserTableFilter : PaginationFilter
    {
      public string Keyword { get; set; }
    }
}
