using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.ClientDto_s.ClientServiceDto
{

    public class NotificationResponseDto
    {
        public Guid NotificationId { get; set; }
        public Guid RecipientId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string NotificationType { get; set; }
        public bool IsUrgent { get; set; }
        public DateTime SentAt { get; set; }
    }

}
