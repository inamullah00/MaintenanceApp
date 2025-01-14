using Application.Interfaces.IUnitOFWork;
using Maintenance.Application.Common.Constants;
using Maintenance.Application.Dto_s.DashboardDtos.Order_Limit_PerformanceReportin_Dtos;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.Order_Limit_PerformanceReportin_interfaces;
using Maintenance.Application.Services.Admin.SetOrderLimit_Performance_Report_Specification;
using Maintenance.Application.Services.Freelance;
using Maintenance.Application.Wrapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Repositories.ServiceImplemention.DashboardServiceImplemention
{
    public class AdminFreelancerService : IAdminFreelancerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminFreelancerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Check if the freelancer can accept new orders
        public async Task<Result<CanAcceptNewOrderResponseDto>> CanAcceptNewOrderAsync(string freelancerId, CancellationToken cancellationToken)
        {
            try
            {
                // Validate the freelancer ID
                if (string.IsNullOrWhiteSpace(freelancerId))
                {
                    return Result<CanAcceptNewOrderResponseDto>.Failure("Freelancer ID cannot be null or empty.", StatusCodes.Status400BadRequest);
                }

                // Call the repository to check if the freelancer can accept new orders
                var (canAccept, freelancer, message) = await _unitOfWork.AdminFreelancerRepository.CanAcceptNewOrderAsync(freelancerId, cancellationToken);

                // Check if freelancer is found
                if (freelancer == null)
                {
                    return Result<CanAcceptNewOrderResponseDto>.Failure("Freelancer not found.", StatusCodes.Status404NotFound);
                }

                // If freelancer cannot accept new orders, return the appropriate message
                if (!canAccept)
                {
                    return Result<CanAcceptNewOrderResponseDto>.Failure(message ?? "The freelancer cannot accept new orders at the moment.", StatusCodes.Status400BadRequest);
                }

                // Create and populate the response DTO
                var response = new CanAcceptNewOrderResponseDto
                {
                    CanAcceptNewOrder = canAccept,
                    CurrentOrderCount = freelancer.OrdersCompleted ?? 0, // Handling nullable value of OrdersCompleted
                    OrderLimit = freelancer.MonthlyLimit ?? 0 // Handling nullable value of MonthlyLimit
                };

                // Return success response
                return Result<CanAcceptNewOrderResponseDto>.Success(response, "Freelancer can accept new orders.");
            }
            catch (Exception ex)
            {

                // Return error response
                return Result<CanAcceptNewOrderResponseDto>.Failure($"An error occurred: {ex.Message}", StatusCodes.Status500InternalServerError);
            }
        }



        // Generate a report of top freelancers for a specific month
        public async Task<Result<List<TopFreelancerResponseDto>>> GenerateTopPerformersReportAsync(int month, CancellationToken cancellationToken)
        {
            try
            {
                // Call the repository to get the top freelancers
                var freelancers = await _unitOfWork.AdminFreelancerRepository.GenerateTopPerformersReportAsync(month, cancellationToken);

                if (freelancers == null || freelancers.Count == 0)
                {
                    return Result<List<TopFreelancerResponseDto>>.Failure("No top performers found.", StatusCodes.Status500InternalServerError);
                }

                var response = freelancers.Select(f => new TopFreelancerResponseDto
                {
                    FreelancerId = f.Id.ToString(),
                    //FullName = $"{f.FirstName} {f.LastName}",
                    //Rating = f.Rating ?? 0,
                    //OrdersCompleted = f.OrdersCompletedThisMonth
                }).ToList();

                return Result<List<TopFreelancerResponseDto>>.Success(response, "Top performers report generated successfully.");
            }
            catch (Exception ex)
            {
                return Result<List<TopFreelancerResponseDto>>.Failure($"An error occurred: {ex.Message}", StatusCodes.Status500InternalServerError);
            }
        }



        // Set the order limit for a freelancer
        public async Task<Result<bool>> SetOrderLimitAsync(string freelancerId, SetOrderLimitRequestDto request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(freelancerId))
                {
                    return Result<bool>.Failure("Invalid freelancer ID.", StatusCodes.Status400BadRequest);
                }

                // Validate request
                if (request.OrderLimit <= 0)
                {
                    return Result<bool>.Failure("Order limit must be greater than zero.", StatusCodes.Status400BadRequest);
                }

                // Call the repository method to set the order limit
                var success = await _unitOfWork.AdminFreelancerRepository.SetOrderLimitAsync(freelancerId, request.OrderLimit, cancellationToken);

                if (!success)
                {
                    return Result<bool>.Failure("Failed to set the order limit.", StatusCodes.Status500InternalServerError);
                }

                return Result<bool>.Success(true, "Order limit set successfully.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"An error occurred: {ex.Message}", StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<Result<List<FreelancerPerformanceReportResponseDto>>> GeneratePerformanceReportAsync(string freelancerId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch performance data from the repository
                var reportData = await _unitOfWork.AdminFreelancerRepository.GeneratePerformanceReportAsync(freelancerId, startDate,endDate, cancellationToken);

                if (reportData == null)
                {
                    return Result<List<FreelancerPerformanceReportResponseDto>>.Failure("No data found for the specified freelancer and month.", StatusCodes.Status404NotFound);
                }

                // Map the data to the response DTO
                //var reportResponse = new FreelancerPerformanceReportResponseDto
                //{
                //    FreelancerId = freelancerId,
                //    TotalOrdersCompleted = reportData.TotalOrdersCompleted,
                //    TotalEarnings = reportData.TotalEarnings,
                //    AverageRating = reportData.AverageRating,
                //    CompletedOrders = reportData.CompletedOrders,
                //    PendingOrders = reportData.PendingOrders
                //};

                return Result<List<FreelancerPerformanceReportResponseDto>>.Success(reportData, "Performance report generated successfully.");
            }
            catch (Exception ex)
            {
                return Result<List<FreelancerPerformanceReportResponseDto>>.Failure($"An error occurred: {ex.Message}", StatusCodes.Status500InternalServerError);
            }
        }
    }
}
