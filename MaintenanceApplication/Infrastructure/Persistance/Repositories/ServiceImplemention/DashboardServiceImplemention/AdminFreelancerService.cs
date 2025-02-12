using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Maintenance.Application.Common;
using Maintenance.Application.Dto_s.DashboardDtos.Order_Limit_PerformanceReportin_Dtos;
using Maintenance.Application.Exceptions;
using Maintenance.Application.Helper;
using Maintenance.Application.Services.Admin.FreelancerSpecification;
using Maintenance.Application.Services.Admin.FreelancerSpecification.Specification;
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.FreelancerEntites;
using Maintenance.Domain.Entity.FreelancerEntities;
using Microsoft.AspNetCore.Http;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention.DashboardServiceImplemention
{
    public class AdminFreelancerService : IAdminFreelancerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileUploaderService _fileUploaderService;
        private readonly IPasswordService _passwordService;
        private readonly string _baseImageUrl;
        public AdminFreelancerService(IUnitOfWork unitOfWork, IMapper mapper, IFileUploaderService fileUploaderService, IPasswordService passwordService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileUploaderService = fileUploaderService;
            _passwordService = passwordService;
            _baseImageUrl = _fileUploaderService.GetImageBaseUrl();

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
                City = freelancer.City,
                Address = freelancer.Address,
                Bio = freelancer.Bio,
                Note = freelancer.Note,
                FreelancerServiceIds = freelancer.FreelancerServices?.Select(fs => fs.ServiceId).ToList() ?? new List<Guid>(),
                ProfilePicture = !string.IsNullOrEmpty(freelancer.ProfilePicture) ? _baseImageUrl + freelancer.ProfilePicture : string.Empty,
                CivilIdString = !string.IsNullOrEmpty(freelancer.CivilID) ? _baseImageUrl + freelancer.CivilID : string.Empty,
            };

        }

        public async Task CreateFreelancerAsync(FreelancerCreateViewModel model, CancellationToken cancellationToken)
        {
            var existingEmail = await _unitOfWork.AdminFreelancerRepository.GetFreelancerByEmailAsync(model.Email, cancellationToken);
            if (existingEmail != null) throw new CustomException($"Duplicate Email {model.Email}");

            var existingPhoneNumber = await _unitOfWork.AdminFreelancerRepository.GetFreelancerByPhoneNumberAsync(model.PhoneNumber, model.CountryId, cancellationToken);
            if (existingPhoneNumber != null) throw new CustomException($"Duplicate Mobile Number {model.PhoneNumber}");

            var country = await _unitOfWork.CountryRepository.GetByIdAsync(model.CountryId) ?? throw new CustomException("Country Not Found");

            var freelancer = new Freelancer
            {
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Bio = model.Bio,
                DateOfBirth = model.DateOfBirth,
                ExperienceLevel = model.ExperienceLevel,
                Note = model.Note,
                Status = AccountStatus.Approved,
                Address = model.Address,
                City = model.City,
                Country = country,
                IsType = UserType.Freelancer,

            };
            freelancer.Password = _passwordService.HashPassword(model.Password);
            if (model.ProfilePictureFile != null)
            {
                var imageFileName = await _fileUploaderService.SaveFileAsync(model.ProfilePictureFile, "Freelancer");
                freelancer.ProfilePicture = imageFileName;
            }
            if (model.CivilID != null)
            {
                var civilIdFileName = await _fileUploaderService.SaveFileAsync(model.CivilID, "Freelancer/CivilIDs");
                freelancer.CivilID = civilIdFileName;
            }
            foreach (var freelancerServiceId in model.FreelancerServiceIds)
            {
                var service = await _unitOfWork.AdminServiceRepository.GetServiceByIdAsync(freelancerServiceId) ?? throw new CustomException("Service not found.");
                var freelancerService = new FreelancerService
                {
                    FreelancerId = freelancer.Id,
                    ServiceId = service.Id
                };
                freelancer.FreelancerServices.Add(freelancerService);
            }
            var createResult = await _unitOfWork.AdminFreelancerRepository.AddFreelancerAsync(freelancer, cancellationToken);
            if (!createResult) throw new CustomException("Failed to create freelancer.");
        }


        public async Task EditFreelancerAsync(FreelancerEditViewModel model, CancellationToken cancellationToken)
        {
            var freelancer = await _unitOfWork.AdminFreelancerRepository
                .GetFreelancerByIdAsync(model.Id, cancellationToken)
                ?? throw new CustomException("Freelancer not found.");

            var country = await _unitOfWork.CountryRepository
                .GetByIdAsync(model.CountryId)
                ?? throw new CustomException("Country Not Found");

            var existingEmail = await _unitOfWork.AdminFreelancerRepository
                .GetFreelancerByEmailAsync(model.Email, cancellationToken);
            if (existingEmail != null && existingEmail.Id != model.Id)
                throw new CustomException($"Duplicate Email {model.Email}");

            var existingPhoneNumber = await _unitOfWork.AdminFreelancerRepository
                .GetFreelancerByPhoneNumberAsync(model.PhoneNumber, model.CountryId, cancellationToken);
            if (existingPhoneNumber != null && existingPhoneNumber.Id != model.Id)
                throw new CustomException($"Duplicate MobileNumber {model.PhoneNumber}");

            // Update freelancer details
            freelancer.FullName = model.FullName;
            freelancer.PhoneNumber = model.PhoneNumber;
            freelancer.Email = model.Email;
            freelancer.City = model.City;
            freelancer.Address = model.Address;
            freelancer.DateOfBirth = model.DateOfBirth;
            freelancer.Bio = model.Bio;
            freelancer.Note = model.Note;
            freelancer.Country = country;
            freelancer.ExperienceLevel = model.ExperienceLevel;

            // Handle Profile Picture Upload
            if (model.ProfilePictureFile != null)
            {
                if (!string.IsNullOrEmpty(freelancer.ProfilePicture))
                    _fileUploaderService.RemoveFile(freelancer.ProfilePicture);

                var imageFileName = await _fileUploaderService.SaveFileAsync(model.ProfilePictureFile, "Freelancer");
                freelancer.ProfilePicture = imageFileName;
            }

            // Handle Civil ID Upload
            if (model.CivilId != null)
            {
                if (!string.IsNullOrEmpty(freelancer.CivilID))
                    _fileUploaderService.RemoveFile(freelancer.CivilID);

                var civilIdFileName = await _fileUploaderService.SaveFileAsync(model.CivilId, "Freelancer/CivilIDs");
                freelancer.CivilID = civilIdFileName;
            }

            // Ensure FreelancerServices is updated via repository
            if (model.FreelancerServiceIds != null)
            {
                await _unitOfWork.AdminFreelancerRepository.UpdateFreelancerServicesAsync(freelancer, model.FreelancerServiceIds, cancellationToken);
            }

            // Save the changes
            var updateResult = await _unitOfWork.AdminFreelancerRepository.UpdateFreelancer(freelancer, cancellationToken);
            if (!updateResult)
                throw new CustomException("Failed to update freelancer.");
        }




        public async Task ApproveFreelancerAsync(Guid id, CancellationToken cancellationToken)
        {
            var freelancer = await _unitOfWork.AdminFreelancerRepository.GetFreelancerByIdAsync(id, cancellationToken) ?? throw new CustomException("Freelancer not found.");
            if (freelancer.Status == AccountStatus.Approved) throw new CustomException("Freelancer is already approved.");
            freelancer.MarkAsApproved();
            var updateResult = await _unitOfWork.AdminFreelancerRepository.Approve(freelancer, cancellationToken);
            if (!updateResult) throw new CustomException("Failed to approve freelancer.");
        }

        public async Task SuspendFreelancerAsync(Guid id, CancellationToken cancellationToken)
        {
            var freelancer = await _unitOfWork.AdminFreelancerRepository.GetFreelancerByIdAsync(id, cancellationToken) ?? throw new CustomException("Freelancer not found.");
            if (freelancer.Status == AccountStatus.Suspended) throw new CustomException("Freelancer is already suspended.");
            freelancer.MarkAsSuspended();
            var updateResult = await _unitOfWork.AdminFreelancerRepository.Suspend(freelancer, cancellationToken);
            if (!updateResult) throw new CustomException("Failed to suspend freelancer.");
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
