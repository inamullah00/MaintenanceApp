using Maintenance.Application.Dto_s.DashboardDtos.Order_Limit_PerformanceReportin_Dtos;
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;

namespace Maintenance.Application.Services.Admin.FreelancerSpecification
{
    public interface IAdminFreelancerService
    {
        // Request DTO: SetOrderLimitDto
        Task<Result<bool>> SetOrderLimitAsync(string freelancerId, SetOrderLimitRequestDto request, CancellationToken cancellationToken);

        // Response DTO: CanAcceptNewOrderDto
        Task<Result<CanAcceptNewOrderResponseDto>> CanAcceptNewOrderAsync(string freelancerId, CancellationToken cancellationToken);

        // Response DTO: FreelancerPerformanceReportDto
        Task<Result<List<FreelancerPerformanceReportResponseDto>>> GeneratePerformanceReportAsync(string freelancerId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken);

        // Response DTO: List<TopFreelancerDto>
        Task<Result<List<TopFreelancerResponseDto>>> GenerateTopPerformersReportAsync(int month, CancellationToken cancellationToken);

        Task<PaginatedResponse<FreelancerResponseViewModel>> GetFilteredFreelancersAsync(FreelancerFilterViewModel filter);
        Task EditFreelancerAsync(FreelancerEditViewModel model, CancellationToken cancellationToken);
        Task<FreelancerEditViewModel> GetFreelancerForEditAsync(Guid id, CancellationToken cancellationToken);
        Task ApproveFreelancerAsync(Guid id, CancellationToken cancellationToken);
        Task SuspendFreelancerAsync(Guid id, CancellationToken cancellationToken);
        Task CreateFreelancerAsync(FreelancerCreateViewModel model, CancellationToken cancellationToken);
    }
}