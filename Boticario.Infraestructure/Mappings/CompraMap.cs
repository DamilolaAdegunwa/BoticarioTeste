using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Boticario.Domain.Entities;

namespace Boticario.Infraestructure.Mappings
{
    public class CompraMap : IEntityTypeConfiguration<Compra>
    {
        public void Configure(EntityTypeBuilder<Compra> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Codigo).IsRequired();
            builder.Property(f => f.Valor).IsRequired();
            builder.Property(f => f.Data).IsRequired();
            builder.Property(f => f.Status).IsRequired();
            builder.HasOne(pt => pt.Revendedor)
            .WithMany(p => p.Compras)
            .HasForeignKey(pt => pt.Revendedor_Id);
        }
    }
}
