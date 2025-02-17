using Maintenance.Application.Dto_s.ClientDto_s.FeedbackDto;
using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Application.Dto_s.FreelancerDto_s.FreelancerPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.FreelancerDto_s
{
    public class FreelancerCompanyDetailsResponseDto
    {

        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public ExperienceLevel ExperienceLevel { get; set; }
        public UserType IsType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string? CivilID { get; set; }
        public string? CompanyLicense { get; set; }
        public string? PreviousWork { get; set; }
        public string? Note { get; set; }
        public AccountStatus Status { get; set; }
        public Guid? CountryId { get; set; }
        public List<BidResponseDto> Bids { get; set; }
        public List<PackageResponseDto> Packages { get; set; }
        public List<OrderResponseDto> FreelancerOrders { get; set; }
        public List<FeedbackResponseDto> ClientFeedbacks { get; set; }
    }

}
