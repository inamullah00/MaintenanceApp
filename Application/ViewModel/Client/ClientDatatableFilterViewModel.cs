﻿namespace Maintenance.Application.ViewModel
{
    public class ClientDatatableFilterViewModel
    {
        public int start { get; set; }
        public int length { get; set; }
        public int draw { get; set; }
        public string? FullName { get; set; }
        public AccountStatus? AccountStatus { get; set; }
    }
}
