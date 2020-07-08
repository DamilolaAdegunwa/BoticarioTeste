using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Boticario.Domain.Entities;
using Boticario.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Boticario.Infraestructure.Repositories
{
    public class CompraRepository : Repository<Compra, int>, ICompraRepository
    {
        private readonly BoticarioContext _context;

        public CompraRepository(BoticarioContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<Compra>> ListAllAsync()
        {
            return await _context.Compras
                .Include(x=>x.Revendedor).ToListAsync();
        }
    }
}
