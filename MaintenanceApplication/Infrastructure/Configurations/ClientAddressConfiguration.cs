using Maintenance.Domain.Entity.ClientEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Configurations
{

    public class ClientAddressConfiguration : IEntityTypeConfiguration<ClientAddress>
    {
        public void Configure(EntityTypeBuilder<ClientAddress> builder)
        {
            // Define primary key
            builder.HasKey(a => a.Id);

            // One-to-Many Relationship with Client
            builder.HasOne(a => a.Client)
                   .WithMany(c => c.ClientAddresses)
                   .HasForeignKey(a => a.ClientId);
        }
    }
}
