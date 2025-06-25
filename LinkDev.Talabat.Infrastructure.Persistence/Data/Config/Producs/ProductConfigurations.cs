using LinkDev.Talabat.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Config.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Config.Producs
{
    internal class ProductConfigurations : BaseAuditableEntityConfigurations<Product, int>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.Property(product => product.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(product => product.Description).IsRequired();

            builder.Property(product => product.Price)
                .HasColumnType("decimal(9,2)");

            builder.HasOne(product => product.Brand)
                .WithMany()
                .HasForeignKey(product => product.BrandId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(product =>product.Category)
                .WithMany()
                .HasForeignKey(product => product.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);


        }

    }
}
