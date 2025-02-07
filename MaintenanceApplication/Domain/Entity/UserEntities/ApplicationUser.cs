using Maintenance.Domain.Entity.Dashboard;
using Maintenance.Domain.Entity.FreelancerEntites;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.UserEntities
{
    public class ApplicationUser : IdentityUser
    {

        public string? FullName { get; set; }
        public ICollection<Dispute> Disputes { get; set; }
        public void UnBlockUser()
        {
            AccessFailedCount = 0;
            LockoutEnd = DateTime.Now.AddDays(-1);
        }

        public void BlockUser()
        {
            AccessFailedCount = 1000;
            LockoutEnd = DateTime.Now.AddYears(4);
        }
    }
}