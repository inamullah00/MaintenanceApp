using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{

    public enum Role
    {
        Client = 1,        // 1 for Client
        Freelancer = 2     // 2 for Freelancer
    }

    // Assigning explicit values to UserStatus enum
    public enum UserStatus
    {
        Pending = 1,       // 1 for Pending
        Approved = 2,      // 2 for Approved
        Suspended = 3,     // 3 for Suspended
        Rejected = 4       // 4 for Rejected
    }

    // Assigning explicit values to ContactPreference enum
    public enum ContactPreference
    {
        Phone = 1,         // 1 for Phone
        Email = 2,         // 2 for Email
        Both = 3           // 3 for Both
    }

    // Assigning explicit values to OtpStatus enum
    public enum OtpStatus
    {
        Used = 1,          // 1 for Used
        Unused = 2         // 2 for Unused
    }

    // Assigning explicit values to LoginStatus enum
    public enum LoginStatus
    {
        Success = 1,       // 1 for Success
        Failure = 2        // 2 for Failure
    }
}
