using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Boticario.Domain.Entities;

namespace Boticario.Infraestructure.Mappings
{
    public class RevendedorMap : IEntityTypeConfiguration<Revendedor>
    {
        public void Configure(EntityTypeBuilder<Revendedor> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.NomeCompleto).IsRequired();
            builder.Property(f => f.Email).IsRequired();
            builder.Property(f => f.Senha).IsRequired();
            builder.Property(f => f.CPF).IsRequired();
        }
    }
}
