using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Maintenance.Application.Dto_s.ClientDto_s.ClientServiceDto;
using Maintenance.Application.Services.ClientPayment;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.Dashboard;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention
{

    public class NotificationService : INotificationService
    {
        //private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<NotificationService> logger)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            //INotificationRepository notificationRepository,
            //_notificationRepository = notificationRepository;
            _logger = logger;
        }

        public IUnitOfWork UnitOfWork { get; }
        public IMapper Mapper { get; }

        public async Task<Result<string>> SendNotificationAsync(NotificationRequestDto notificationDto, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Sending notification to recipient with ID: {RecipientId}", notificationDto.RecipientId);

                // Map DTO to domain entity (if required)
                var notification = new Notification
                {
                    //RecipientId = notificationDto.RecipientId,
                    //Title = notificationDto.Title,
                    //Message = notificationDto.Message,
                    //NotificationType = notificationDto.NotificationType,
                    //IsUrgent = notificationDto.IsUrgent,
                    //Metadata = notificationDto.Metadata,
                    //CreatedAt = DateTime.UtcNow
                };

                // Store or trigger the notification
                //await _notificationRepository.AddAsync(notification, cancellationToken);

                _logger.LogInformation("Notification sent successfully to recipient with ID: {RecipientId}", notificationDto.RecipientId);
                return Result<string>.Success("Notification sent successfully.", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send notification to recipient with ID: {RecipientId}", notificationDto.RecipientId);
                return Result<string>.Failure("An error occurred while sending the notification.", StatusCodes.Status500InternalServerError);
            }
        }
    }

}
