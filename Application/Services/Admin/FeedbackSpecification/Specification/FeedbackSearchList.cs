using Ardalis.Specification;
using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.Admin.FeedbackSpecification.Specification
{
    public class FeedbackSearchList:Specification<Feedback>
    {
        public FeedbackSearchList(string Keyword = "")
        {
            
        }
    }
}
