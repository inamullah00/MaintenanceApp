using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.FreelancerDto_s
{
    public class BidUpdateDto
    {

        [Range(1, double.MaxValue, ErrorMessage = "Bid amount must be greater than zero.")]
        public decimal? BidAmount { get; set; } 

    }
}
