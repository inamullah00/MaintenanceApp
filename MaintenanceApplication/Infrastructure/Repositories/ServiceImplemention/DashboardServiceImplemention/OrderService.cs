using Application.Dto_s.ClientDto_s;
using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using MailKit.Search;
using Maintenance.Application.Common.Constants;
using Maintenance.Application.Dto_s.ClientDto_s;
using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Application.Dto_s.FreelancerDto_s;
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
                OrderSearchList Specification = new(Keyword);
                var orders = await _unitOfWork.OrderRepository.GetAllAsync(cancellationToken, Specification);
                var orderDtos = _mapper.Map<List<OrderResponseDto>>(orders); 
                return Result<List<OrderResponseDto>>.Success(orderDtos, SuccessMessages.OrderFetchedSuccessfully, StatusCodes.Status200OK); 
        }
        #endregion

        #region Get Order by ID
        public async Task<Result<OrderResponseDto>> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken)
        {
                if (id == Guid.Empty)
                {
                    return Result<OrderResponseDto>.Failure( ErrorMessages.InvalidOrderId, StatusCodes.Status400BadRequest);
                }

                var order = await _unitOfWork.OrderRepository.GetByIdAsync(id, cancellationToken);
                if (order == null)
                {
                    return Result<OrderResponseDto>.Failure(ErrorMessages.OrderNotFound, StatusCodes.Status404NotFound);
                }
                var orderDto = _mapper.Map<OrderResponseDto>(order); 
                return Result<OrderResponseDto>.Success(orderDto, SuccessMessages.OrderFetchedSuccessfully, StatusCodes.Status200OK);
        
        }
        #endregion

        #region Assign Order
        public async Task<Result<string>> AssignOrderAsync(Guid id, AssignOrderRequestDto assignOrderDto, CancellationToken cancellationToken)
        {
                if (id == Guid.Empty || assignOrderDto == null)
                {
                    return Result<string>.Failure( ErrorMessages.InvalidOrderData, StatusCodes.Status400BadRequest);
                }

                var order = await _unitOfWork.OrderRepository.GetByIdAsync(id, cancellationToken);
                if (order == null)
                {
                    return Result<string>.Failure(ErrorMessages.OrderNotFound, StatusCodes.Status404NotFound);
                }

                if (order.Status != OrderStatus.Pending)
                {
                    return Result<string>.Failure(ErrorMessages.OrderAssignmentFailed, StatusCodes.Status400BadRequest);
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

                return Result<string>.Success(SuccessMessages.OrderAssignedSuccessfully, StatusCodes.Status200OK); 
      
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

                var order = await _unitOfWork.OrderRepository.GetEntityByIdAsync(id, cancellationToken);
                if (order ==null)
                {
                    return Result<string>.Failure(
                     ErrorMessages.ResourceNotFound,
                     StatusCodes.Status404NotFound
                 );
                }

                // Update the order status (you can uncomment and add your business logic here)
                order.Status = updateOrderStatusDto.OrderStatus;
                await _unitOfWork.OrderRepository.UpdateAsync(order, cancellationToken);

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
      
                if (createOrderRequestDto == null)
                {
                    return Result<OrderResponseDto>.Failure( ErrorMessages.InvalidOrderData,StatusCodes.Status400BadRequest );
                }

                var order = _mapper.Map<Order>(createOrderRequestDto);

                var orderRes = await _unitOfWork.OrderRepository.CreateAsync(order, cancellationToken);

                if(!orderRes)
                {
                    return Result<OrderResponseDto>.Failure(ErrorMessages.OrderCreationFailed, StatusCodes.Status500InternalServerError);
                }

                var orderResponse = _mapper.Map<OrderResponseDto>(order);

                return Result<OrderResponseDto>.Success( orderResponse,SuccessMessages.OrderCreatedSuccessfully,StatusCodes.Status201Created);
        }

        #endregion


       public async Task<Result<string>> CompleteWorkAsync(Guid id, CompleteWorkDTORequest WorkDTORequest, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetEntityByIdAsync(id, cancellationToken);

                if (order == null)
                {
                    return Result<string>.Failure(
                        ErrorMessages.ResourceNotFound,
                        StatusCodes.Status404NotFound
                    );
                }

                // Mark the order as completed
                order.Status = OrderStatus.Completed;  // Assuming `Completed` is a status in your Order entity
                order.CompletedDate = WorkDTORequest.CompletionDate;  // You can add a completion date here


                // Update the order in the database
              var isSuccess=  await _unitOfWork.OrderRepository.UpdateAsync(order, cancellationToken);

                if (!isSuccess) 
                {
               
                   return Result<string>.Failure(ErrorMessages.OrderStatusUpdateFailed, StatusCodes.Status500InternalServerError);
                    
                }

                var orderResponse = _mapper.Map<OrderResponseDto>(order);  // Map order to the DTO

                return Result<string>.Success(orderResponse.Id.ToString(), StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(
                    $"Error completing work: {ex.Message}",
                    StatusCodes.Status500InternalServerError
                );
            }
        }

      public async Task<Result<string>> ApproveOrderAsync(Guid orderId, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetEntityByIdAsync(orderId, cancellationToken);

                if (order == null)
                {
                    return Result<string>.Failure(
                        ErrorMessages.ResourceNotFound,
                        StatusCodes.Status404NotFound
                    );
                }

                // Mark the order as completed
                //order.IsOrdeCompleted = true;


                // Update the order in the database
                var isSuccess = await _unitOfWork.OrderRepository.UpdateAsync(order, cancellationToken);

                if (!isSuccess)
                {

                    return Result<string>.Failure(ErrorMessages.OrderStatusUpdateFailed, StatusCodes.Status500InternalServerError);

                }

                var orderResponse = _mapper.Map<OrderResponseDto>(order);  // Map order to the DTO

                return Result<string>.Success(orderResponse.Id.ToString(), StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(
                    $"Error completing work: {ex.Message}",
                    StatusCodes.Status500InternalServerError
                );
            }
        }

        public async Task<Result<string>> RejectOrderAsync(Guid id, RejectOrderRequestDTO RejectOrderRequest, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetEntityByIdAsync(id, cancellationToken);

                if (order == null)
                {
                    return Result<string>.Failure(
                        ErrorMessages.ResourceNotFound,
                        StatusCodes.Status404NotFound
                    );
                }

                // Mark the order as completed
                order.Status = RejectOrderRequest.OrderStatus;
              


                // Update the order in the database
                var isSuccess = await _unitOfWork.OrderRepository.UpdateAsync(order, cancellationToken);

                if (!isSuccess)
                {

                    return Result<string>.Failure(ErrorMessages.OrderStatusUpdateFailed, StatusCodes.Status500InternalServerError);

                }

                var orderResponse = _mapper.Map<OrderResponseDto>(order);  // Map order to the DTO

                return Result<string>.Success(orderResponse.Id.ToString(), StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(
                    $"Error completing work: {ex.Message}",
                    StatusCodes.Status500InternalServerError
                );
            }
        }

        #endregion


    }
}
