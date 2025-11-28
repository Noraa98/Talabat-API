using LinkDev.Talabat.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.Talabat.Infrastructure.Persistence._Identity.Config
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(a => a.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(a => a.FirstName)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

            builder.Property(a => a.LastName)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

            builder.Property(a => a.Street)
                   .HasColumnType("varchar")
                   .HasMaxLength(100);

            builder.Property(a => a.City)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

            builder.Property(a => a.Country)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

            builder.ToTable("Addresses");
        }
    }
}
