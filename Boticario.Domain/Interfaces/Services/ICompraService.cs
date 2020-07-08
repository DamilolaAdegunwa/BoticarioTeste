using Boticario.Domain.Entities;
using Boticario.Domain.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boticario.Domain.Interfaces.Services
{
    public interface ICompraService
    {
        Task<Compra> InsertAsync(Compra entity, string cpf);
        Task<ICollection<Compra>> ListAsync();
        Task<Resultado> GetCashBackAsync(string cpf);
    }
}
