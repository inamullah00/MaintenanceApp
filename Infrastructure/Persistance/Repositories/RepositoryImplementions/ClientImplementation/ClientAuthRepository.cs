using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.ClientInterfaces;
using Maintenance.Domain.Entity.ClientEntities;
using Maintenance.Domain.Entity.UserEntities;
using Maintenance.Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Persistance.Repositories.RepositoryImplementions.ClientImplementation
{
    public class ClientAuthRepository : IClientAuthRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ClientAuthRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Client> AddClientAsync(Client client,CancellationToken cancellationToken)
        {
            await _dbContext.Clients.AddAsync(client);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return client;
        }

        public async Task<ClientOtp> AddClientOTP(ClientOtp otp,CancellationToken cancellationToken)
        {
            await _dbContext.ClientOtps.AddAsync(otp);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return otp;
        }

        public async Task<bool> ClientExistsAsync(Guid clientId , CancellationToken cancellationToken)
        {
            return await _dbContext.Clients.AnyAsync(c => c.Id == clientId);
        }

        public async Task<bool> DeleteClientAsync(Guid clientId , CancellationToken cancellationToken)
        {
            var client = await _dbContext.Clients.FindAsync(clientId);
            if (client == null) return false;

            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<ClientOtp?> DeleteClientOTP(Guid Id, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.ClientOtps.FirstOrDefaultAsync(o => o.Id == Id);
            if (entity != null)
            {
                _dbContext.ClientOtps.Remove(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            return entity;
        }

        public async Task<Client?> GetClientByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(c => c.Email == email, cancellationToken);
        }

        public async Task<Client?> GetClientByIdAsync(Guid clientId, CancellationToken cancellationToken)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId, cancellationToken);
        }

        public async Task<ClientOtp?> GetClientOTPByEmail(string Email)
        {
            return await _dbContext.ClientOtps.FirstOrDefaultAsync(o => o.Email == Email);

        }

        public async Task<ClientOtp?> GetValidOtpAsync(int otp, CancellationToken cancellationToken)
        {
            return await _dbContext.ClientOtps.Where(o => o.OtpCode == otp && o.ExpiresAt > DateTime.UtcNow)
                            .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Client> UpdateClientAsync(Client client)
        {
            _dbContext.Clients.Update(client);
            await _dbContext.SaveChangesAsync();
            return client;
        }

    

        //Get Valid OTP 
        public async Task<ClientOtp?> GetValidClientOTPByEmail(string email, CancellationToken cancellationToken)
        {
            return await _dbContext.ClientOtps
                .Where(otp => otp.Email == email && !otp.IsUsed && otp.ExpiresAt > DateTime.UtcNow)
                .OrderByDescending(otp => otp.ExpiresAt) // Get the latest valid OTP
                .FirstOrDefaultAsync(cancellationToken);
        }

        //Get Recently Expired OTP

        public async Task<ClientOtp?> GetRecentlyExpiredOTP(string email, CancellationToken cancellationToken)
        {
            return await _dbContext.ClientOtps
                .Where(otp => otp.Email == email && otp.ExpiresAt < DateTime.UtcNow) // Only expired OTPs
                .OrderByDescending(otp => otp.ExpiresAt) // Get the most recently expired OTP
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Client> UpdateClientAsync(Client client, CancellationToken cancellationToken)
        {
             _dbContext.Clients.Update(client);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return client;
        }

        public async Task<ClientOtp> UpdateClientOTP(ClientOtp otp, CancellationToken cancellationToken)
        {
            _dbContext.ClientOtps.Update(otp); // No need to await here
            await _dbContext.SaveChangesAsync(cancellationToken);
            return otp;
        }
    }
}
