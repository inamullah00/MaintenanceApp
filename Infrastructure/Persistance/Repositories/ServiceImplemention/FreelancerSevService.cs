﻿using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Maintenance.Application.Common.Constants;
using Maintenance.Application.Dto_s.ClientDto_s.ClientOrderDtos;
using Maintenance.Application.Dto_s.FreelancerDto_s;
using Maintenance.Application.Dto_s.FreelancerDto_s.FreelancerPackage;
using Maintenance.Application.Services.Admin.OrderSpecification.Specification;
using Maintenance.Application.Services.Freelance;
using Maintenance.Application.Services.Freelance.Specification;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.Dashboard;
using Maintenance.Domain.Entity.FreelancerEntites;
using Maintenance.Domain.Entity.FreelancerEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention
{
    public class FreelancerSevService : IFreelancerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FreelancerSevService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        #region DeleteBidAsync
        public async Task<Result<string>> DeleteBidAsync(Guid bidId)
        {

            if (bidId == Guid.Empty)
            {
                return Result<string>.Failure(ErrorMessages.InvalidFreelancerBidId, StatusCodes.Status400BadRequest);
            }

            SearchBidByMatchingId Specification = new SearchBidByMatchingId(bidId);

            var bid = await _unitOfWork.FreelancerRepository.GetByIdAsync(Specification);

            if (bid == null)
            {
                return Result<string>.Failure(ErrorMessages.BidNotFound, StatusCodes.Status404NotFound);
            }
            var ExistingBid = _mapper.Map<Bid>(bid);

            var result = await _unitOfWork.FreelancerRepository.RemoveAsync(ExistingBid);
            if (!result)
            {
                return Result<string>.Failure(ErrorMessages.FreelancerBidDeletionFailed, StatusCodes.Status500InternalServerError);
            }

            await _unitOfWork.SaveChangesAsync();
            return Result<string>.Success(SuccessMessages.FreelancerBidDeleted, StatusCodes.Status200OK);
        }
        #endregion

        #region GetBidsByFreelancerAsync
        public async Task<Result<BidResponseDto>> GetBidByFreelancerAsync(Guid freelancerId)
        {
            if (freelancerId == Guid.Empty)
            {
                return Result<BidResponseDto>.Failure(ErrorMessages.InvalidFreelancerId, StatusCodes.Status400BadRequest);
            }

            FreelancerBidSearchList Specification = new FreelancerBidSearchList(freelancerId.ToString());

            var bids = await _unitOfWork.FreelancerRepository.GetByIdAsync(Specification);

            if (bids == null)
            {
                return Result<BidResponseDto>.Failure(ErrorMessages.FreelancerBidNotFound, StatusCodes.Status404NotFound);
            }

            var bidResponseDto = _mapper.Map<BidResponseDto>(bids);

            return Result<BidResponseDto>.Success(bidResponseDto, SuccessMessages.FreelancerBidFetched, StatusCodes.Status200OK);
        }
        #endregion

        #region SubmitBidAsync
        public async Task<Result<string>> SubmitBidAsync(BidRequestDto bidRequestDto ,CancellationToken cancellationToken)
        {

            if (bidRequestDto == null)
            {
                return Result<string>.Failure(ErrorMessages.InvalidFreelancerBidData, StatusCodes.Status400BadRequest);
            }

            // Validate Offered Service
            var offeredService = await _unitOfWork.OfferedServiceRepository.GetByIdAsync(bidRequestDto.OfferedServiceId, cancellationToken);
            if (offeredService == null)
            {
                return Result<string>.Failure(ErrorMessages.ServiceNotFound, StatusCodes.Status404NotFound);
            }

            // Validate Freelancer
            var freelancer = await _unitOfWork.FreelancerAuthRepository.GetFreelancerByIdAsync(bidRequestDto.FreelancerId, cancellationToken);
            if (freelancer == null)
            {
                return Result<string>.Failure(ErrorMessages.FreelancerNotFound, StatusCodes.Status404NotFound);
            }

            // Create Bid
            var bid = new Bid
            {
                Id = Guid.NewGuid(),
                OfferedServiceId = bidRequestDto.OfferedServiceId,
                FreelancerId = bidRequestDto.FreelancerId,
                BidStatus = BidStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            var bidPackages = new List<BidPackage>();

            // Validate & Add Packages
            foreach (var packageDto in bidRequestDto.BidPackages)
            {
                var package = await _unitOfWork.FreelancerRepository.GetPackageByIdAsync(packageDto.PackageId, cancellationToken);
                if (package == null)
                {
                    return Result<string>.Failure($"Package with ID {packageDto.PackageId} not found", StatusCodes.Status404NotFound);
                }



                // ✅ Check in Database Before Adding
                bool packageExists = await _unitOfWork.FreelancerRepository.ExistsAsync<BidPackage>(
                    bp => bp.BidId == bid.Id && bp.PackageId == packageDto.PackageId,
                    cancellationToken
                );

                if (!packageExists) // ✅ Only add if it doesn't already exist in the database
                {
                    bidPackages.Add(new BidPackage
                    {
                        Id = Guid.NewGuid(),
                        BidId = bid.Id,
                        PackageId = packageDto.PackageId
                    });
                }

            }

            // Assign packages to bid
            bid.BidPackages = bidPackages;

            // Save Bid & BidPackages to Database
            await _unitOfWork.FreelancerRepository.CreateAsync(bid, cancellationToken);

            
            //await _unitOfWork.FreelancerRepository.CreateRangeAsync(bidPackages, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success(SuccessMessages.FreelancerBidCreated, StatusCodes.Status200OK);
        }
        #endregion

        #region UpdateBidAsync
        public async Task<Result<string>> UpdateBidAsync(BidUpdateDto bidUpdateDto, Guid freelancerId)
        {

            if (freelancerId == Guid.Empty || bidUpdateDto == null)
            {
                return Result<string>.Failure(ErrorMessages.InvalidFreelancerBidData, StatusCodes.Status400BadRequest);
            }


            SearchBidByMatchingId Specification = new SearchBidByMatchingId(freelancerId.ToString());

            var bid = await _unitOfWork.FreelancerRepository.GetByIdAsync(Specification);
            if (bid == null)
            {
                return Result<string>.Failure(ErrorMessages.BidNotFound, StatusCodes.Status404NotFound);
            }
            var entity = _mapper.Map<Bid>(bidUpdateDto);

            var result = await _unitOfWork.FreelancerRepository.UpdateAsync(entity, freelancerId);
            if (!result.Item1)
            {
                return Result<string>.Failure(ErrorMessages.FreelancerBidUpdateFailed, StatusCodes.Status500InternalServerError);
            }

            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success(SuccessMessages.FreelancerBidUpdated, StatusCodes.Status200OK);
        }

        #endregion

        #region GetBidsByFreelancers on Specified Client Service
        public async Task<Result<List<FreelancerBidsResponseDto>>> GetBidsByFreelancerAsync(Guid offeredServiceId, CancellationToken cancellationToken)
        {
            BidSearchList Specification = new BidSearchList(offeredServiceId);
            var bids = await _unitOfWork.FreelancerRepository.GetAllAsync(cancellationToken, Specification);

            return Result<List<FreelancerBidsResponseDto>>.Success(bids, $"{bids.Count} {SuccessMessages.FreelancerBidFetched}", StatusCodes.Status200OK);
        }
        #endregion

        #region ApproveBidAsync
        public async Task<Result<string>> ApproveBidAsync(Guid Id, ApproveBidRequestDto bidRequestDto)
        {
            if (Id == Guid.Empty || bidRequestDto == null)
            {
                return Result<string>.Failure(
                    ErrorMessages.InvalidFreelancerBidData,
                    HttpResponseCodes.BadRequest
                );
            }

            SearchBidByMatchingId Specification = new SearchBidByMatchingId(Id);
            var bid = await _unitOfWork.FreelancerRepository.GetByIdAsync(Specification);

            if (bid == null)
            {
                return Result<string>.Failure(ErrorMessages.BidNotFound, StatusCodes.Status404NotFound);
            }

            var entity = _mapper.Map<Bid>(bidRequestDto);

            var result = await _unitOfWork.FreelancerRepository.ApproveBidAsync(entity, Id);
            if (!result.Item1)
            {
                return Result<string>.Failure(ErrorMessages.FreelancerBidApprovalFailed, StatusCodes.Status500InternalServerError);
            }


            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success(SuccessMessages.FreelancerBidAccepted, StatusCodes.Status200OK);
        }

        #endregion

        #region FilterFreelancersAsync
        public async Task<Result<List<FilteredFreelancerResponseDto>>> FilterFreelancersAsync(FilterFreelancerRequestDto filterRequestDto, CancellationToken cancellationToken)
        {
            // Create a specification for filtering freelancers based on pricing and rating
            var specification = new FreelancerFilterSpecification(
                filterRequestDto.MinPrice,
                filterRequestDto.MaxPrice,
                filterRequestDto.MinRating,
                filterRequestDto.MaxRating
            );

            // Fetch the filtered list of freelancers using the specification
            var freelancers = await _unitOfWork.FreelancerRepository.GetByFilterAsync(cancellationToken, specification);

            // Map the fetched freelancers to the response DTO
            var freelancerList = _mapper.Map<List<FilteredFreelancerResponseDto>>(freelancers);

            // Return a successful result with the list of freelancers
            return Result<List<FilteredFreelancerResponseDto>>.Success(
                freelancerList,
                $"{freelancers.Count} {SuccessMessages.FreelancersFiltered}",
                StatusCodes.Status200OK
            );
        }
        #endregion

        #region GetRequestedServicesAsync
        public async Task<Result<List<RequestedServiceResponseDto>>> GetRequestedServicesAsync(CancellationToken cancellationToken, string? keyword)
        {
            var specification = new RequestedServiceSpecification(keyword);
            var services = await _unitOfWork.OfferedServiceRepository.GetRequestedServicesAsync(specification, cancellationToken);

            if (services == null || services.Count == 0)
            {
                return Result<List<RequestedServiceResponseDto>>.Success("No requested services found.", StatusCodes.Status200OK);
            }

            var serviceDtos = _mapper.Map<List<RequestedServiceResponseDto>>(services);
            return Result<List<RequestedServiceResponseDto>>.Success(serviceDtos, "Requested services fetched successfully.", StatusCodes.Status200OK);
        }
        #endregion

        #region GetOrdersByStatusAsync
        public async Task<Result<List<OrderStatusResponseDto>>> GetOrdersByStatusAsync(OrderStatus status, CancellationToken cancellationToken)
        {

            var specification = new OrderStatusSearchList(status);
            var orderStatusResponse = await _unitOfWork.OrderRepository.GetOrdersByStatusAsync(cancellationToken, specification);

            if (orderStatusResponse == null || orderStatusResponse.Count == 0)
            {
                return Result<List<OrderStatusResponseDto>>.Success("No requested services found.", StatusCodes.Status200OK);
            }

            return Result<List<OrderStatusResponseDto>>.Success(orderStatusResponse, "Orders Fetched Successfuly.", StatusCodes.Status200OK);


        }
        #endregion

        public async Task<Result<Package>> GetPackageByIdAsync(Guid packageId, CancellationToken cancellationToken)
        {
            var package = await _unitOfWork.FreelancerRepository.GetPackageByIdAsync(packageId, cancellationToken);

            if (package == null)
            {

                return Result<Package>.Failure("Package not found.", StatusCodes.Status404NotFound);
            }

            return Result<Package>.Success(package, "Package fetched successfully.", StatusCodes.Status200OK);
        }

        public async Task<Result<List<Package>>> GetPackagesAsync(CancellationToken cancellationToken)
        {
            var packages = await _unitOfWork.FreelancerRepository.GetAllPackagesAsync(cancellationToken);

            if (packages == null || !packages.Any())
            {
                return Result<List<Package>>.Failure("No packages found.", StatusCodes.Status404NotFound);
            }

            //_mapper.Map(package)

            return Result<List<Package>>.Success(packages, "Packages fetched successfully.");
        }

        public async Task<Result<Package>> CreatePackageAsync(CreatePackageRequestDto packageRequestDto, CancellationToken cancellationToken)
        {

            if (packageRequestDto == null)
            {
                return Result<Package>.Failure(ErrorMessages.InvalidOrEmpty, StatusCodes.Status400BadRequest);
            }

            var packageEntity = _mapper.Map<Package>(packageRequestDto);

            var result = await _unitOfWork.FreelancerRepository.CreatePackageAsync(packageEntity, cancellationToken);

            if (result == null)
            {
                return Result<Package>.Failure(ErrorMessages.PackageCreationFailed, StatusCodes.Status500InternalServerError);
            }


            return Result<Package>.Success(SuccessMessages.PackageCreated, StatusCodes.Status201Created);
        }

        public async Task<Result<Package>> UpdatePackageAsync(Guid packageId, Package package, CancellationToken cancellationToken)
        {
            var existingPackage = await _unitOfWork.FreelancerRepository.GetPackageByIdAsync(packageId, cancellationToken);

            if (existingPackage == null)
            {
                return Result<Package>.Failure("Package not found.", StatusCodes.Status404NotFound);
            }

            // Update properties
            existingPackage.Name = package.Name;
            existingPackage.Price = package.Price;
            existingPackage.OfferDetails = package.OfferDetails;

            await _unitOfWork.SaveChangesAsync();

            return Result<Package>.Success(existingPackage, "Package updated successfully.");
        }

        public async Task<Result<bool>> DeletePackageAsync(Guid packageId, CancellationToken cancellationToken)
        {
            var package = await _unitOfWork.FreelancerRepository.GetPackageByIdAsync(packageId, cancellationToken);

            if (package == null)
            {
                return Result<bool>.Failure("Package not found.", StatusCodes.Status404NotFound);
            }

            await _unitOfWork.FreelancerRepository.DeletePackageAsync(package.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync();
            return Result<bool>.Success(true, "Package deleted successfully.");
        }

        public async Task<Result<List<FreelancerCompanyDetailsResponseDto>>> GetFreelancerDetailsAsync(Guid FreelancerId, CancellationToken cancellationToken)
        {
            if(FreelancerId == Guid.Empty)
            {
                return Result<List<FreelancerCompanyDetailsResponseDto>>.Failure(ErrorMessages.InvalidFreelancerId, StatusCodes.Status400BadRequest);
            }

            var specification = new FreelancerCompanyDetailsSpecification(FreelancerId);
            var FreelancerDetails = await _unitOfWork.FreelancerRepository.GetFreelancerDetailsAsync(specification, cancellationToken);

            if (FreelancerDetails == null)
            {
                return Result<List<FreelancerCompanyDetailsResponseDto>>.Failure("Freelancer Details not found.", StatusCodes.Status404NotFound);
            }

            return Result<List<FreelancerCompanyDetailsResponseDto>>.Success(FreelancerDetails,SuccessMessages.OperationSuccessful, StatusCodes.Status200OK);

        }

        public async Task<Result<List<FreelancerCompanyDetailsResponseDto>>> GetCompanyDetailsAsync(Guid ComponyId, CancellationToken cancellationToken)
        {
            if (ComponyId == Guid.Empty)
            {
                return Result<List<FreelancerCompanyDetailsResponseDto>>.Failure(ErrorMessages.InvalidApiKey, StatusCodes.Status400BadRequest);
            }

            var specification = new FreelancerCompanyDetailsSpecification(ComponyId);
            var CompanyDetails = await _unitOfWork.FreelancerRepository.GetCompanyDetailsAsync(specification, cancellationToken);

            if (CompanyDetails == null)
            {
                return Result<List<FreelancerCompanyDetailsResponseDto>>.Failure("Compony Details not found.", StatusCodes.Status404NotFound);
            }

            return Result<List<FreelancerCompanyDetailsResponseDto>>.Success(CompanyDetails, SuccessMessages.OperationSuccessful, StatusCodes.Status200OK);

        }
    }
}
