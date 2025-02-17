using Ardalis.Specification;
using Domain.Entity.UserEntities;
using Maintenance.Application.Dto_s.DashboardDtos.Order_Limit_PerformanceReportin_Dtos;
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.FreelancerEntites;
using Maintenance.Domain.Entity.FreelancerEntities;

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
        Task<Freelancer> GetFreelancerByEmailAsync(string email, CancellationToken cancellationToken);
        Task<Freelancer> GetFreelancerByPhoneNumberAsync(string phoneNumber, Guid? countryId, CancellationToken cancellationToken);
        Task<bool> Approve(Freelancer freelancer, CancellationToken cancellationToken = default);
        Task<bool> Suspend(Freelancer freelancer, CancellationToken cancellationToken = default);
        Task<bool> AddFreelancerAsync(Freelancer freelancer, CancellationToken cancellationToken = default);
        Task<List<FreelancerService>> GetFreelancerServicesAsync(Guid freelancerId, CancellationToken cancellationToken);
        Task<Freelancer?> GetFreelancerByIdAsync(Guid id, CancellationToken cancellationToken, bool trackChanges = false);
        Task<bool> UpdateFreelancer(Freelancer freelancer, CancellationToken cancellationToken = default);
        Task UpdateFreelancerServicesAsync(Freelancer freelancer, List<Guid> newServiceIds, CancellationToken cancellationToken);
        Task<PaginatedResponse<CompanyResponseViewModel>> GetFilteredCompaniesAsync(FreelancerFilterViewModel filter, ISpecification<Freelancer>? specification = null);
    }
}
