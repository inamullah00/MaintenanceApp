using Maintenance.Domain.Entity.ClientEntities;
using Maintenance.Domain.Entity.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.ClientInterfaces
{
    public interface IClientAuthRepository
    {

        // ===================== CLIENT METHODS =====================

        // Add a new client to the database
        Task<Client> AddClientAsync(Client client, CancellationToken cancellationToken);

        // Get client by ID
        Task<Client> GetClientByIdAsync(Guid clientId, CancellationToken cancellationToken);

        // Get client by email
        Task<Client?> GetClientByEmailAsync(string email, CancellationToken cancellationToken);

        // Update client details
        Task<Client> UpdateClientAsync(Client client, CancellationToken cancellationToken);

        // Delete client (soft delete or permanent delete)
        Task<bool> DeleteClientAsync(Guid clientId, CancellationToken cancellationToken);

        // Check if a client exists by ID
        Task<bool> ClientExistsAsync(Guid clientId, CancellationToken cancellationToken);


        // ===================== OTP METHODS =====================

        // Add a client OTP to the database
        Task<ClientOtp> AddClientOTP(ClientOtp otp, CancellationToken cancellationToken);

        // Update client OTP
        Task<ClientOtp> UpdateClientOTP(ClientOtp otp, CancellationToken cancellationToken);

        // Delete client OTP
        Task<ClientOtp?> DeleteClientOTP(Guid id, CancellationToken cancellationToken);

        // Get valid OTP by email
        Task<ClientOtp?> GetValidClientOTPByEmail(string email, CancellationToken cancellationToken);

        // Get valid OTP for client authentication
        Task<ClientOtp?> GetValidOtpAsync(int otp, CancellationToken cancellationToken);

        // Get recently expired OTP
        Task<ClientOtp?> GetRecentlyExpiredOTP(string email, CancellationToken cancellationToken);

    }
}
