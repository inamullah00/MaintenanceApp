using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Security
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool ValidatePassword(string password, string hashedPassword);
    }
}
