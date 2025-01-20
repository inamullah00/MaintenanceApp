using Maintenance.Application.Common.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Common.Utility
{
    public class NanoHelper
    {

        public static string GenerateOrderByString(PaginationFilter filter)
        {

            // translate a dynamic TanstackColumnOrder List into a string format readable by ardalis specification OrderBy
            // string format example: ('Name,-Supplier,Property.Name,Price') -prefix denotes Descending
            string sortingString = "";
            int numberOfColumns = filter.Sorting.Count;

            int count = 1;
            foreach (TanstackColumnOrder sortColumn in filter.Sorting)
            {
                if (sortColumn.Desc) // prepend a minus if order equals descending
                {
                    sortingString += "-" + sortColumn.Id;
                }
                else
                {
                    sortingString += sortColumn.Id;
                }

                if (count != numberOfColumns) // append comma if not last in series
                {
                    sortingString += ",";
                }
                count++;
            }
            return sortingString;
        }
    }
}
