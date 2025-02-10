using Ardalis.Specification;
using Domain.Entity.UserEntities;
using Maintenance.Application.Dto_s.DashboardDtos.Order_Limit_PerformanceReportin_Dtos;
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.FreelancerEntites;

namespace Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.FreelancerInterfaces
{
    public interface IAdminFreelancerRepository
    {
        // Method to set the order limit for a freelancer
        Task<bool> SetOrderLimitAsync(string freelancerId, int orderLimit, CancellationToken cancellationToken);

        // Method to check if the freelancer can accept new orders
        Task<(bool, ApplicationUser?, string? Message)> CanAcceptNewOrderAsync(string freelancerId, CancellationToken cancellationToken);

        // Method to generate a freelancer's performance report for a specific month
        Task<List<FreelancerPerformanceReportResponseDto>> GeneratePerformanceReportAsync(string freelancerId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken);

        // Method to generate a list of top freelancers for a given month
        Task<List<ApplicationUser>> GenerateTopPerformersReportAsync(int month, CancellationToken cancellationToken);
        Task<PaginatedResponse<FreelancerResponseViewModel>> GetFilteredFreelancersAsync(FreelancerFilterViewModel filter, ISpecification<Freelancer>? specification = null);
        Task<Freelancer?> GetFreelancerByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Freelancer> GetFreelancerByEmailAsync(string email, CancellationToken cancellationToken);
        Task<Freelancer> GetFreelancerByPhoneNumberAsync(string phoneNumber, Guid? countryId, CancellationToken cancellationToken);
        Task<bool> UpdateFreelancer(Freelancer freelancer, CancellationToken cancellationToken = default);
        Task<bool> Approve(Freelancer freelancer, CancellationToken cancellationToken = default);
        Task<bool> Suspend(Freelancer freelancer, CancellationToken cancellationToken = default);
        Task<bool> AddFreelancerAsync(Freelancer freelancer, CancellationToken cancellationToken = default);
    }
}
