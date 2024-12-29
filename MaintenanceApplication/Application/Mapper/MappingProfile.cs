using Application.Dto_s.ClientDto_s;
using Application.Dto_s.ClientDto_s.ClientServiceCategoryDto;
using Application.Dto_s.UserDto_s;
using AutoMapper;
using Domain.Entity.UserEntities;
using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Application.Dto_s.FreelancerDto_s;
using Maintenance.Domain.Entity.Client;
using Maintenance.Domain.Entity.Dashboard;
using Maintenance.Domain.Entity.Freelancer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDetailsResponseDto>().ReverseMap();
            CreateMap<UserDetailsResponseDto, ApplicationUser>().ReverseMap();


            CreateMap<OfferedService,OfferedServiceResponseDto>().ReverseMap();
            CreateMap<OfferedServiceRequestDto, OfferedService>().ReverseMap();
            CreateMap<OfferedUpdateRequestDto, OfferedService>().ReverseMap();

            CreateMap<OfferedServiceCategory, OfferedServiceCategoryResponseDto>().ReverseMap();
            CreateMap<OfferedServiceCategoryRequestDto, OfferedServiceCategory>().ReverseMap();
            CreateMap<OfferedServiceCategoryUpdateDto, OfferedServiceCategory>().ReverseMap();


            CreateMap<Bid, BidResponseDto>().ReverseMap();
            CreateMap<BidRequestDto, Bid>().ReverseMap();
            CreateMap<BidUpdateDto, Bid>().ReverseMap();
            CreateMap<Bid,ApproveBidRequestDto>().ReverseMap();

            CreateMap<Order, OrderResponseDto>().ReverseMap();
            CreateMap<CreateOrderRequestDto, Order>().ReverseMap();
            CreateMap<UpdateOrderStatusDto, Order>().ReverseMap();
        }
    }
}
