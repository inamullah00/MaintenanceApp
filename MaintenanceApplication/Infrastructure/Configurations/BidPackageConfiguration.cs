using Maintenance.Domain.Entity.FreelancerEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Configurations
{

    public class BidPackageConfiguration : IEntityTypeConfiguration<BidPackage>
    {
        public void Configure(EntityTypeBuilder<BidPackage> builder)
        {
            builder.ToTable("BidPackages");

            builder.HasKey(bp => new { bp.BidId, bp.PackageId }); // Composite primary key

            builder.HasOne(bp => bp.Bid)
                .WithMany(b => b.BidPackages)
                .HasForeignKey(bp => bp.BidId);

            builder.HasOne(bp => bp.Package)
                .WithMany(p => p.BidPackages)
                .HasForeignKey(bp => bp.PackageId);
        }
    }
}
