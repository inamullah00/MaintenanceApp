using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Maintenance.Application.Dto_s.DashboardDtos.Order_Limit_PerformanceReportin_Dtos;
using Maintenance.Application.Exceptions;
using Maintenance.Application.Services.Admin.FreelancerSpecification;
using Maintenance.Application.Services.Admin.FreelancerSpecification.Specification;
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;
using Microsoft.AspNetCore.Http;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention.DashboardServiceImplemention
{
    public class AdminFreelancerService : IAdminFreelancerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminFreelancerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedResponse<FreelancerResponseViewModel>> GetFilteredFreelancersAsync(FreelancerFilterViewModel filter)
        {
            var specification = new FreelancerSearchList(filter);
            return await _unitOfWork.AdminFreelancerRepository.GetFilteredFreelancersAsync(filter, specification);
        }

        public async Task<FreelancerEditViewModel> GetFreelancerForEditAsync(Guid id, CancellationToken cancellationToken)
        {
            var freelancer = await _unitOfWork.AdminFreelancerRepository.GetFreelancerByIdAsync(id, cancellationToken) ?? throw new CustomException("Freelancer not found.");
            return new FreelancerEditViewModel
            {
                Id = freelancer.Id,
                FullName = freelancer.FullName,
                Email = freelancer.Email,
                PhoneNumber = freelancer.PhoneNumber,
                CountryId = freelancer.CountryId,
                ExperienceLevel = freelancer.ExperienceLevel,
                Status = freelancer.Status.ToString(),
                DateOfBirth = freelancer.DateOfBirth.Date,
                Bio = freelancer.Bio,
                PreviousWork = freelancer.PreviousWork
            };

        }
        public async Task EditFreelancerAsync(FreelancerEditViewModel model, CancellationToken cancellationToken)
        {
            var freelancer = await _unitOfWork.AdminFreelancerRepository.GetFreelancerByIdAsync(model.Id, cancellationToken) ?? throw new CustomException("Freelancer not found.");

            var country = await _unitOfWork.CountryRepository.GetByIdAsync(model.CountryId);
            var existingEmail = await _unitOfWork.AdminFreelancerRepository.GetFreelancerByEmailAsync(model.Email, cancellationToken);
            if (existingEmail != null && existingEmail.Id != model.Id) throw new CustomException($"Duplicate Email {model.Email}");
            var existingPhoneNumber = await _unitOfWork.AdminFreelancerRepository.GetFreelancerByPhoneNumberAsync(model.Email, model.CountryId, cancellationToken);
            if (existingPhoneNumber != null && existingPhoneNumber.Id != model.Id) throw new CustomException($"Duplicate MobileNumber {model.PhoneNumber}");
            _mapper.Map(model, freelancer);
            freelancer.Country = country;

            var updateResult = await _unitOfWork.AdminFreelancerRepository.UpdateFreelancerAsync(freelancer, cancellationToken);
            if (!updateResult) throw new CustomException("Failed to update freelancer.");
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
                    //CurrentOrderCount = freelancer.OrdersCompleted ?? 0, // Handling nullable value of OrdersCompleted
                    //OrderLimit = freelancer.MonthlyLimit ?? 0 // Handling nullable value of MonthlyLimit
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
                var reportData = await _unitOfWork.AdminFreelancerRepository.GeneratePerformanceReportAsync(freelancerId, startDate, endDate, cancellationToken);

                if (reportData == null)
                {
                    return Result<List<FreelancerPerformanceReportResponseDto>>.Failure("No data found for the specified freelancer and month.", StatusCodes.Status404NotFound);
                }

                return Result<List<FreelancerPerformanceReportResponseDto>>.Success(reportData, "Performance report generated successfully.");
            }
            catch (Exception ex)
            {
                return Result<List<FreelancerPerformanceReportResponseDto>>.Failure($"An error occurred: {ex.Message}", StatusCodes.Status500InternalServerError);
            }
        }
    }
}
