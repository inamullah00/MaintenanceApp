using Application.Dto_s.ClientDto_s;
using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Maintenance.Application.Common.Constants;
using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.AdminOrderInterfaces;
using Maintenance.Application.Services.Admin.OrderSpecification;
using Maintenance.Application.Services.Admin.OrderSpecification.Specification;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.Dashboard;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Repositories.ServiceImplemention.DashboardServiceImplemention
{

    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Order Management

        #region Get All Orders
        public async Task<Result<List<OrderResponseDto>>> GetAllOrdersAsync(CancellationToken cancellationToken, string? Keyword = "")
        {
            try
            {
                OrderSearchList Specification = new(Keyword);
                var orders = await _unitOfWork.OrderRepository.GetAllAsync(cancellationToken, Specification);
                var orderDtos = _mapper.Map<List<OrderResponseDto>>(orders); 
                return Result<List<OrderResponseDto>>.Success(orderDtos, SuccessMessages.OperationSuccessful, StatusCodes.Status200OK); 
            }
            catch (Exception ex)
            {
                return Result<List<OrderResponseDto>>.Failure($"Error fetching orders: {ex.Message}", "An error occurred", StatusCodes.Status500InternalServerError); 
            }
        }
        #endregion

        #region Get Order by ID
        public async Task<Result<OrderResponseDto>> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return Result<OrderResponseDto>.Failure(
                     ErrorMessages.InvalidOrEmptyId,
                     HttpResponseCodes.BadRequest
                 );
                }

                var order = await _unitOfWork.OrderRepository.GetByIdAsync(id, cancellationToken);
                if (order == null)
                {
                    return Result<OrderResponseDto>.Failure(ErrorMessages.ResourceNotFound, StatusCodes.Status404NotFound);
                }
                var orderDto = _mapper.Map<OrderResponseDto>(order); 
                return Result<OrderResponseDto>.Success(orderDto, SuccessMessages.OperationSuccessful, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return Result<OrderResponseDto>.Failure($"Error fetching order by ID: {ex.Message}", "An error occurred", StatusCodes.Status500InternalServerError); 
            }
        }
        #endregion

        #region Assign Order
        public async Task<Result<string>> AssignOrderAsync(Guid id, AssignOrderRequestDto assignOrderDto, CancellationToken cancellationToken)
        {
            try
            {
                if (id == Guid.Empty || assignOrderDto == null)
                {
                    return Result<string>.Failure( ErrorMessages.InvalidOrEmpty, StatusCodes.Status400BadRequest);
                }

                var order = await _unitOfWork.OrderRepository.GetByIdAsync(id, cancellationToken);
                if (order == null)
                {
                    return Result<string>.Failure(ErrorMessages.ResourceNotFound, StatusCodes.Status404NotFound);
                }

                if (order.Status != OrderStatus.Pending)
                {
                    return Result<string>.Failure("Cannot assign this order.", "The order is not in a pending state.", HttpResponseCodes.BadRequest);
                }


                order.FreelancerId = assignOrderDto.FreelancerId;
                order.Status = OrderStatus.InProgress;
                order.UpdatedAt = DateTime.UtcNow;

                var orderEntity = _mapper.Map<Order>(order);
               
                var isUpdated = await _unitOfWork.OrderRepository.UpdateFieldsAsync(
                    orderEntity,
                    new[] { nameof(order.FreelancerId), nameof(order.Status), nameof(order.UpdatedAt) },
                    cancellationToken
                );

                return Result<string>.Success(SuccessMessages.OrderAssignedSuccessfully, HttpResponseCodes.OK); 
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"Error assigning order: {ex.Message}", "An error occurred", HttpResponseCodes.InternalServerError); // HTTP 500 Internal Server Error
            }
        }

        #endregion

        #region Update Order Status
        public async Task<Result<string>> UpdateOrderStatusAsync(Guid id, UpdateOrderStatusDto updateOrderStatusDto, CancellationToken cancellationToken)
        {
            try
            {
                if (id == Guid.Empty || updateOrderStatusDto ==null) 
                {
                   return Result<string>.Failure(ErrorMessages.InvalidOrEmptyId,  StatusCodes.Status400BadRequest);
                   
                }

                var order = await _unitOfWork.OrderRepository.GetByIdAsync(id, cancellationToken);
                if (order ==null)
                {
                    return Result<string>.Failure(
                     ErrorMessages.ResourceNotFound,
                     StatusCodes.Status404NotFound
                 );
                }

                // Update the order status (you can uncomment and add your business logic here)
                // order.Status = updateOrderStatusDto.Status;
                // await _unitOfWork.OrderRepository.UpdateAsync(order, cancellationToken);

                // Return success result with a message
                return Result<string>.Success(SuccessMessages.OrderStatusUpdatedSuccessfully, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"Error updating order status: {ex.Message}", "An error occurred", StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region Create Order
        public async Task<Result<OrderResponseDto>> CreateOrderAsync(CreateOrderRequestDto createOrderRequestDto, CancellationToken cancellationToken)
        {
            try
            {

                if (createOrderRequestDto == null)
                {
                    return Result<OrderResponseDto>.Failure(
                     ErrorMessages.InvalidOrEmpty,
                     HttpResponseCodes.BadRequest
                 );
                }

                var order = _mapper.Map<Order>(createOrderRequestDto);

                var orderRes = await _unitOfWork.OrderRepository.CreateAsync(order, cancellationToken);

                var orderResponse = _mapper.Map<OrderResponseDto>(order);

                return Result<OrderResponseDto>.Success(
                    orderResponse,
                   SuccessMessages.OrderPlacedSuccessfully,
                    StatusCodes.Status201Created
                );
            }
            catch (Exception ex)
            {
                return Result<OrderResponseDto>.Failure(
                    $"Error placing order: {ex.Message}",
                    "An error occurred while placing the order",
                   StatusCodes.Status500InternalServerError
                );
            }
        }


        #endregion

        #endregion


    }
}
