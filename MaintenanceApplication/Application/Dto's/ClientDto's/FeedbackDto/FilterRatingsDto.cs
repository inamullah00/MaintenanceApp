using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.ClientDto_s.FeedbackDto
{

    public class FilterRatingsDto
    {
        [FromQuery] public Guid? FreelancerId { get; set; }
        [FromQuery] public int? MinRating { get; set; }
        [FromQuery] public int? MaxRating { get; set; }
        [FromQuery] public DateTime? FromDate { get; set; }
        [FromQuery] public DateTime? ToDate { get; set; }
        [FromQuery] public Guid? ServiceId { get; set; }
    }

}
