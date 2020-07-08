using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Boticario.Domain.Entities;
using Boticario.Infraestructure.Mappings;
using Boticario.Infraestructure.Seed;

namespace Boticario.Infraestructure
{
    public class BoticarioContext : DbContext
    {
        public BoticarioContext(DbContextOptions<BoticarioContext> options) : base(options) { }
        public BoticarioContext()
        {

        }

        public DbSet<Revendedor> Revendedores { get; set; }
        public DbSet<Compra> Compras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompraMap());
            modelBuilder.ApplyConfiguration(new RevendedorMap());

            BoticarioSeed.Seed(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
