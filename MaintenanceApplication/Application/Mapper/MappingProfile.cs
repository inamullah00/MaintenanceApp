using Application.Dto_s.ClientDto_s;
using Application.Dto_s.ClientDto_s.ClientServiceCategoryDto;
using Application.Dto_s.UserDto_s;
using AutoMapper;
using Domain.Entity.UserEntities;
using Maintenance.Application.Dto_s.ClientDto_s.FeedbackDto;
using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Application.Dto_s.DashboardDtos.ContentDtos;
using Maintenance.Application.Dto_s.DashboardDtos.DisputeDtos;
using Maintenance.Application.Dto_s.FreelancerDto_s;
using Maintenance.Application.Dto_s.FreelancerDto_s.FreelancerPackage;
using Maintenance.Application.Dto_s.UserDto_s;
using Maintenance.Application.Dto_s.UserDto_s.FreelancerAuthDtos;
using Maintenance.Application.ViewModel;
using Maintenance.Domain.Entity.ClientEntities;
using Maintenance.Domain.Entity.Dashboard;
using Maintenance.Domain.Entity.FreelancerEntites;
using Maintenance.Domain.Entity.FreelancerEntities;

namespace Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDetailsResponseDto>().ReverseMap();
            CreateMap<UserDetailsResponseDto, ApplicationUser>().ReverseMap();


            CreateMap<Freelancer, FreelancerRegistrationDto>().ReverseMap();
            CreateMap<Freelancer, FreelancerProfileDto>().ReverseMap();
            CreateMap<Freelancer, FreelancerEditViewModel>().ReverseMap();


            //Package
            CreateMap<Package, PackageResponseDto>().ReverseMap();
            CreateMap<CreatePackageRequestDto, Package>().ReverseMap();
            CreateMap<UpdatePackageRequestDto, Package>().ReverseMap();




            CreateMap<OfferedService, OfferedServiceResponseDto>().ReverseMap();
            CreateMap<OfferedServiceRequestDto, OfferedService>().ReverseMap();
            CreateMap<OfferedUpdateRequestDto, OfferedService>().ReverseMap();

            CreateMap<OfferedServiceCategory, OfferedServiceCategoryResponseDto>().ReverseMap();
            CreateMap<OfferedServiceCategoryRequestDto, OfferedServiceCategory>().ReverseMap();
            CreateMap<OfferedServiceCategoryUpdateDto, OfferedServiceCategory>().ReverseMap();


            CreateMap<Bid, BidResponseDto>().ReverseMap();
            CreateMap<BidRequestDto, Bid>().ReverseMap();
            CreateMap<BidUpdateDto, Bid>().ReverseMap();
            CreateMap<Bid, ApproveBidRequestDto>().ReverseMap();

            CreateMap<Order, OrderResponseDto>().ReverseMap();
            CreateMap<CreateOrderRequestDto, Order>().ReverseMap();
            CreateMap<UpdateOrderStatusDto, Order>().ReverseMap();

            #region Account Mapping
            CreateMap<ApplicationUser, UserProfileDto>().ReverseMap();
            CreateMap<ApplicationUser, UserProfileEditDto>().ReverseMap();
            #endregion

            #region Dispute Mapping
            CreateMap<Dispute, DisputeResponseDto>().ReverseMap();
            CreateMap<CreateDisputeRequest, Dispute>().ReverseMap();
            CreateMap<UpdateDisputeRequest, Dispute>().ReverseMap();
            #endregion

            #region Content Mapping
            CreateMap<Content, ContentResponseDto>().ReverseMap();
            CreateMap<CreateContentRequestDto, Content>().ReverseMap();
            CreateMap<UpdateContentRequestDto, Content>().ReverseMap();
            #endregion

            #region Content Mapping
            CreateMap<Feedback, FeedbackResponseDto>().ReverseMap();
            CreateMap<CreateFeedbackRequestDto, Feedback>().ReverseMap();
            CreateMap<UpdateFeedbackRequestDto, Feedback>().ReverseMap();
            #endregion

        }
    }
}
