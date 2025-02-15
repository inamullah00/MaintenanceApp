using Maintenance.Application.Communication;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention
{
    public class EmailService : IEmailService
    {

        private readonly IConfiguration _configuration;


        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;

        }


        public Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            throw new NotImplementedException();
        }

    }
}
