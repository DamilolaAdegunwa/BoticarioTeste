using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Boticario.Domain.Entities;

namespace Boticario.Domain.Interfaces.Services
{
    public interface IRevendedorService
    {
        Task<Revendedor> InsertAsync(Revendedor entity);
    }
}
