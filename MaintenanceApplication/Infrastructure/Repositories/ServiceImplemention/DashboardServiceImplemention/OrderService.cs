using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.AdminOrderInterfaces;
using Maintenance.Application.Interfaces.ServiceInterfaces.DashboardInterfaces.AdminOrderInterafces;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.Dashboard;
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
        public async Task<Result<List<OrderResponseDto>>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetAllAsync(cancellationToken);
                var orderDtos = _mapper.Map<List<OrderResponseDto>>(orders); 
                return Result<List<OrderResponseDto>>.Success(orderDtos, "Orders fetched successfully", 200); 
            }
            catch (Exception ex)
            {
                return Result<List<OrderResponseDto>>.Failure($"Error fetching orders: {ex.Message}", "An error occurred", 500); 
            }
        }
        #endregion

        #region Get Order by ID
        public async Task<Result<OrderResponseDto>> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetByIdAsync(id, cancellationToken);
                if (order == null)
                    return Result<OrderResponseDto>.Failure("Order not found", "No order found with the provided ID", 404); 

                var orderDto = _mapper.Map<OrderResponseDto>(order); 
                return Result<OrderResponseDto>.Success(orderDto, "Order fetched successfully", 200);
            }
            catch (Exception ex)
            {
                // Log exception and handle as needed
                return Result<OrderResponseDto>.Failure($"Error fetching order by ID: {ex.Message}", "An error occurred", 500); // Failure result
            }
        }
        #endregion

        #region Assign Order
        public async Task<Result<string>> AssignOrderAsync(Guid id, AssignOrderRequestDto assignOrderDto, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch the order by ID
                var order = await _unitOfWork.OrderRepository.GetByIdAsync(id, cancellationToken);
                if (order == null)
                {
                    return Result<string>.Failure("Order not found", "Order not found", 404); // HTTP 404 Not Found
                }

                // Check if the order is already assigned or completed
                if (order.Status != OrderStatus.Pending)
                {
                    return Result<string>.Failure("Cannot assign this order.", "The order is not in a pending state.", 400);
                }

                // Assign the freelancer and update the status
                order.FreelancerId = assignOrderDto.FreelancerId;
                order.Status = OrderStatus.InProgress;
                order.UpdatedAt = DateTime.UtcNow;

                var orderEntity = _mapper.Map<Order>(order);
                // Save changes
                var isUpdated = await _unitOfWork.OrderRepository.UpdateFieldsAsync(
                    orderEntity,
                    new[] { nameof(order.FreelancerId), nameof(order.Status), nameof(order.UpdatedAt) },
                    cancellationToken
                );

                // Return success result with a message
                return Result<string>.Success("Order assigned successfully.", "Order assigned", 200); // HTTP 200 OK
            }
            catch (Exception ex)
            {
                // Return failure result with the error message
                return Result<string>.Failure($"Error assigning order: {ex.Message}", "An error occurred", 500); // HTTP 500 Internal Server Error
            }
        }

        #endregion

        #region Update Order Status
        public async Task<Result<string>> UpdateOrderStatusAsync(Guid id, UpdateOrderStatusDto updateOrderStatusDto, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch the order by ID
                var order = await _unitOfWork.OrderRepository.GetByIdAsync(id, cancellationToken);
                if (order == null)
                {
                    return Result<string>.Failure("Order not found", "Order not found", 404); // HTTP 404 Not Found
                }

                // Update the order status (you can uncomment and add your business logic here)
                // order.Status = updateOrderStatusDto.Status;
                // await _unitOfWork.OrderRepository.UpdateAsync(order, cancellationToken);

                // Return success result with a message
                return Result<string>.Success("Order status updated successfully.", "Status updated", 200); // HTTP 200 OK
            }
            catch (Exception ex)
            {
                // Return failure result with the error message
                return Result<string>.Failure($"Error updating order status: {ex.Message}", "An error occurred", 500); // HTTP 500 Internal Server Error
            }
        }
        #endregion


        #region Resolve Order Dispute
        public async Task<Result<string>> ResolveDisputeAsync(Guid id, ResolveDisputeDto resolveDisputeDto, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch the order by ID
                var order = await _unitOfWork.OrderRepository.GetByIdAsync(id, cancellationToken);
                if (order == null)
                {
                    return Result<string>.Failure("Order not found", "Order not found", 404); // HTTP 404 Not Found
                }

                // Logic to resolve the dispute (you can uncomment and add your business logic here)
                // order.DisputeResolved = true;
                // order.DisputeResolutionDetails = resolveDisputeDto.ResolutionDetails;
                // await _unitOfWork.OrderRepository.UpdateAsync(order);

                // Return success result with a message
                return Result<string>.Success("Order dispute resolved successfully.", "Dispute resolved", 200); // HTTP 200 OK
            }
            catch (Exception ex)
            {
                // Return failure result with the error message
                return Result<string>.Failure($"Error resolving dispute: {ex.Message}", "An error occurred", 500); // HTTP 500 Internal Server Error
            }
        }


        public async Task<Result<OrderResponseDto>> CreateOrderAsync(CreateOrderRequestDto createOrderRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                // Map the DTO to the domain entity
                var order = _mapper.Map<Order>(createOrderRequestDto);

                // Create the order asynchronously
                var orderRes = await _unitOfWork.OrderRepository.CreateAsync(order, cancellationToken);

                // Map the created order to the response DTO
                var orderResponse = _mapper.Map<OrderResponseDto>(order);

                // Return the success result with the created order
                return Result<OrderResponseDto>.Success(
                    orderResponse,
                    "Order placed successfully",
                    201 // HTTP Status Code 201 Created
                );
            }
            catch (Exception ex)
            {
                // Return the failure result with an error message and appropriate status code
                return Result<OrderResponseDto>.Failure(
                    $"Error placing order: {ex.Message}",
                    "An error occurred while placing the order",
                    500 // HTTP Status Code 500 Internal Server Error
                );
            }
        }


        #endregion

        #endregion


    }
}
