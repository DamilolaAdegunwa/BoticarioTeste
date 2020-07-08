using Boticario.Domain.Entities;
using Boticario.Domain.Interfaces;
using Boticario.Domain.Interfaces.Repositories;
using Boticario.Domain.Interfaces.Services;
using Boticario.Service.Services;
using System.Threading.Tasks;

namespace Boticario.Service.Services
{
    public class RevendedorService : BaseService, IRevendedorService
    {
        private readonly IRevendedorRepository _repository;
        public RevendedorService(IRevendedorRepository repository,
                            IUnitOfWorkFactory unitOfWorkFactory) : base(unitOfWorkFactory)
        {
            _repository = repository;
        }

        public async Task<Revendedor> InsertAsync(Revendedor entity)
        {
            return await _repository.InsertAsync(entity);
        }
    }
}
