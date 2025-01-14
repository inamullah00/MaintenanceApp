using Domain.Entity.UserEntities;
using Maintenance.Application.Dto_s.DashboardDtos.Order_Limit_PerformanceReportin_Dtos;
using Maintenance.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.Order_Limit_PerformanceReportin_interfaces
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
    }
}
