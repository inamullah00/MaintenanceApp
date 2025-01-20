using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Common.Filters
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<TanstackColumnOrder> Sorting { get; set; }
        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = 10;
            Sorting = new List<TanstackColumnOrder>();
        }
    }

    public class TanstackColumnOrder
    {
        public string Id { get; set; }
        public bool Desc { get; set; }
    }
}
