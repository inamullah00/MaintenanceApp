using Ardalis.Specification;
using Maintenance.Application.Dto_s.ClientDto_s.FeedbackDto;
using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.Admin.FeedbackSpecification.Specification
{
    public class FilterRatingSpecification : Specification<Feedback>
    {
        public FilterRatingSpecification(FilterRatingsDto filterRatingsDto)
        {
            Query.Include(f => f.Client) // Include Client for name
                 .Include(f => f.Order); // Include Order if needed

            if (filterRatingsDto.FreelancerId.HasValue)
            {
                Query.Where(f => f.FeedbackOnFreelancerId == filterRatingsDto.FreelancerId);
            }

            if (filterRatingsDto.ServiceId.HasValue)
            {
                Query.Where(f => f.Order.ServiceId == filterRatingsDto.ServiceId);
            }

            if (filterRatingsDto.MinRating.HasValue)
            {
                Query.Where(f => f.Rating >= filterRatingsDto.MinRating);
            }

            if (filterRatingsDto.MaxRating.HasValue)
            {
                Query.Where(f => f.Rating <= filterRatingsDto.MaxRating);
            }

            if (filterRatingsDto.FromDate.HasValue)
            {
                Query.Where(f => f.CreatedAt >= filterRatingsDto.FromDate);
            }

            if (filterRatingsDto.ToDate.HasValue)
            {
                Query.Where(f => f.CreatedAt <= filterRatingsDto.ToDate);
            }

            Query.OrderByDescending(f => f.CreatedAt); // Latest feedback first

        }


    }
}
