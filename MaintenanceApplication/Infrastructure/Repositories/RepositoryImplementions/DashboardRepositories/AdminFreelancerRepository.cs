using Domain.Entity.UserEntities;
using Infrastructure.Data;
using Maintenance.Application.Dto_s.DashboardDtos.Order_Limit_PerformanceReportin_Dtos;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.AdminOrderInterfaces;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.Order_Limit_PerformanceReportin_interfaces;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.Dashboard;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Repositories.RepositoryImplementions.DashboardRepositories
{
    public class AdminFreelancerRepository : IAdminFreelancerRepository
    {

        private readonly ApplicationDbContext _context;

        public AdminFreelancerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Check if the freelancer can accept new orders
        public async Task<(bool,ApplicationUser?,string? Message)> CanAcceptNewOrderAsync(string freelancerId, CancellationToken cancellationToken)
        {
            // Retrieve freelancer from the database
            var freelancer = await _context.Users
                .FirstOrDefaultAsync(f => f.Id == freelancerId, cancellationToken);

            // If freelancer is not found
            if (freelancer == null)
            {
                return (false, null, "Freelancer not found with the provided ID.");
            }

            // If freelancer has completed zero orders, they can accept new orders
            if (freelancer.OrdersCompleted == 0)
            {
                return (true, freelancer, "The freelancer has no completed orders yet and can accept new orders.");
            }

            // If the freelancer has completed orders but hasn't exceeded the order limit
            if (freelancer.OrdersCompleted <= freelancer.MonthlyLimit)
            {
                return (true, freelancer, $"The freelancer can accept new orders. Current limit is {freelancer.MonthlyLimit} and they have completed {freelancer.OrdersCompleted} orders.");
            }

            // If freelancer has exceeded the order limit
            return (false, freelancer, "The freelancer has exceeded their monthly order limit and cannot accept new orders.");


        }

     
        // Set the order limit for a freelancer
        public async  Task<bool> SetOrderLimitAsync(string freelancerId, int orderLimit, CancellationToken cancellationToken)
        {
            var freelancer = await _context.Users
                .FirstOrDefaultAsync(f => f.Id == freelancerId, cancellationToken);

            if (freelancer == null)
            {
                return false; // Freelancer not found
            }
            // Set the new order limit
            freelancer.MonthlyLimit = orderLimit;

            _context.Users.Update(freelancer);

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

       public async Task<List<FreelancerPerformanceReportResponseDto>> GeneratePerformanceReportAsync(string freelancerId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            try
            {
                // Query the orders based on freelancer and date range
                //var orders = await _context.Orders
                //    .Where(o => o.FreelancerId == freelancerId && o.CompletedDate >= startDate && o.CompletedDate <= endDate)
                //    .ToListAsync(cancellationToken);

                //if (orders == null || orders.Count == 0)
                //{
                //    return null; // No orders found for the freelancer in the specified date range
                //}


                var performanceReport = await (from FreelancerOrders in _context.Orders
                                               join Freelancer in _context.Users
                                               on FreelancerOrders.FreelancerId equals Freelancer.Id
                                               where FreelancerOrders.CompletedDate >= startDate && FreelancerOrders.CompletedDate <= endDate
                                               group FreelancerOrders by Freelancer.Id into g
                                               select new FreelancerPerformanceReportResponseDto
                                               {
                                                   FreelancerId = g.Key,
                                                   FreelancerName = g.Select(f => f.Freelancer.FirstName + " " + f.Freelancer.LastName).FirstOrDefault(),
                                                   TotalOrders = g.Count(),
                                                   TotalEarnings = g.Sum(o => o.FreelancerAmount), // Assuming `FreelancerAmount` is the payment to the freelancer
                                                   //AverageRating = g.Average(o => o.Rat), // Assuming `Rating` is available in the `Orders`
                                                   CompletedOrders = g.Count(o => o.Status == OrderStatus.Completed), // Adjust depending on your `Status` field
                                                   StartDate = startDate,
                                                   EndDate = endDate
                                               }).ToListAsync(cancellationToken);

                return performanceReport;





                // Query feedback related to these orders
                //var feedbacks = await _context.Feedbacks
                //    .Where(f => orders.Select(o => o.Id).Contains(f.OrderId))
                //    .ToListAsync(cancellationToken);

                //// Calculate total earnings, total completed orders, total pending orders
                //var totalEarnings = orders.Sum(o => o.TotalAmount);
                //var totalCompleted = orders.Count(o => o.Status == OrderStatus.Completed);
                //var totalPending = orders.Count(o => o.Status == OrderStatus.Pending);

                //// Calculate average rating from the feedbacks (only consider feedback with a rating)
                //var averageRating = feedbacks.Count > 0
                //    ? feedbacks.Average(f => f.Rating)
                //    : 0;  // Default to 0 if no feedback is available

                //// Create and return the performance report response
                //return new FreelancerPerformanceReportResponseDto
                //{
                //    TotalOrdersCompleted = totalCompleted,
                //    TotalEarnings = totalEarnings,
                //    AverageRating = averageRating,
                //    CompletedOrders = totalCompleted,
                //    PendingOrders = totalPending
                //};
                return null;

            }
            catch (Exception ex)
            {
                // Log the error or handle it as needed
                throw new Exception("Error fetching performance data", ex);
            }
        }

        Task<List<ApplicationUser>> IAdminFreelancerRepository.GenerateTopPerformersReportAsync(int month, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        //public async Task<PerformanceReportDto> GeneratePerformanceReportAsync(Guid freelancerId, DateTime startDate, DateTime endDate)
        //{
        //    var report = new PerformanceReportDto();

        //    // Get orders completed in the specified period
        //    var completedOrders = await _orderRepository.GetCompletedOrdersAsync(freelancerId, startDate, endDate);
        //    report.CompletedOrders = completedOrders.Count;

        //    // Get earnings (sum of payments to the freelancer)
        //    var earnings = await _paymentRepository.GetTotalEarningsAsync(freelancerId, startDate, endDate);
        //    report.TotalEarnings = earnings;

        //    // Get average rating and feedback count
        //    var feedbacks = await _feedbackRepository.GetFeedbacksForFreelancerAsync(freelancerId, startDate, endDate);
        //    report.AverageRating = feedbacks.Average(f => f.Rating);
        //    report.FeedbackCount = feedbacks.Count;

        //    // Order completion rate
        //    var totalAssignedOrders = await _orderRepository.GetAssignedOrdersAsync(freelancerId, startDate, endDate);
        //    report.OrderCompletionRate = (double)report.CompletedOrders / totalAssignedOrders.Count * 100;

        //    // Monthly limit usage
        //    var freelancer = await _userManager.FindByIdAsync(freelancerId.ToString());
        //    report.MonthlyLimitUsage = freelancer.OrdersCompleted.HasValue
        //        ? (freelancer.OrdersCompleted.Value / freelancer.MonthlyLimit.Value) * 100
        //        : 0;

        //    return report;
        //}

    }
}
