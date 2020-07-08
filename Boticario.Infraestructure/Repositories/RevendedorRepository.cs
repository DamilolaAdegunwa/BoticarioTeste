using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Boticario.Domain.Entities;
using Boticario.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Boticario.Infraestructure.Repositories
{
    public class RevendedorRepository : Repository<Revendedor, int>, IRevendedorRepository
    {
        private readonly BoticarioContext _context;

        public RevendedorRepository(BoticarioContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Revendedor> FindAsync(string cpf)
        {
            return await _context.Revendedores
                .FirstOrDefaultAsync(x => x.CPF == cpf);
        }

        public bool ValidateLogin(string email, string senha)
        {
            return  _context.Revendedores
                .Any(p => p.Email == email && p.Senha == senha);
        }
    }
}
