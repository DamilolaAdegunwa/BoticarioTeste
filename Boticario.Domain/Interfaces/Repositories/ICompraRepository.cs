using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Boticario.Domain.Entities;

namespace Boticario.Domain.Interfaces.Repositories
{
    public interface ICompraRepository : IRepository<Compra, int>
    {
        Task<ICollection<Compra>> ListAllAsync();
    }
}
