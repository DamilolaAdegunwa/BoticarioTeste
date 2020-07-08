using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Boticario.Domain.Entities;

namespace Boticario.Domain.Interfaces.Repositories
{
    public interface IRevendedorRepository : IRepository<Revendedor, int>
    {
        bool ValidateLogin(string email, string senha);
        Task<Revendedor> FindAsync(string cpf);
    }
}
