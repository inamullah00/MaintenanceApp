using Maintenance.Domain.Entity.FreelancerEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Security
{
    public interface ITokenService
    {
       public string GenerateToken(Freelancer freelancer);
    }
}
