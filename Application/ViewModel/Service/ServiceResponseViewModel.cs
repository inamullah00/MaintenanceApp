﻿namespace Maintenance.Application.ViewModel
{
    public class ServiceResponseViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsUserCreated { get; set; }
        public bool IsApproved { get; set; }
    }
}
