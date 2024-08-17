using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchMvc.Infra.Data.EntitiesMapping;

public class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Price).HasPrecision(10,2);
        builder.Property(x => x.Stock).IsRequired();
        builder.Property(x => x.Image).HasMaxLength(250);


        builder.HasOne(c => c.Category)
               .WithMany(p => p.Products)
               .HasForeignKey(c => c.CategoryId);

    }
}
