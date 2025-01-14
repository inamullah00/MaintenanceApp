using Maintenance.Application.Dto_s.DashboardDtos.Order_Limit_PerformanceReportin_Dtos;
using Maintenance.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.Admin.SetOrderLimit_Performance_Report_Specification
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
    }

}
