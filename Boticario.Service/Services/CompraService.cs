using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Boticario.Domain.Entities;
using Boticario.Domain.Interfaces;
using Boticario.Domain.Interfaces.Repositories;
using Boticario.Domain.Interfaces.Services;
using Boticario.Infraestructure.Exceptions;
using System.Linq;
using Boticario.Service.Helpers;
using Microsoft.Extensions.Configuration;
using Boticario.Domain.Results;

namespace Boticario.Service.Services
{
    public class CompraService : BaseService, ICompraService
    {
        private IConfiguration _config;
        private readonly ICompraRepository _repository;
        private readonly IRevendedorRepository _revendedorRepository;
        public CompraService(IConfiguration config, ICompraRepository repository,
                             IRevendedorRepository revendedorRepository,
                             IUnitOfWorkFactory unitOfWorkFactory) : base(unitOfWorkFactory)
        {
            _config = config;
            _repository = repository;
            _revendedorRepository = revendedorRepository;
        }

        public async Task<Resultado> GetCashBackAsync(string cpf)
        {
            return await HttpHelper.Get<Resultado>(_config["API:Url"],
                string.Format("{0}{1}", _config["API:value"], cpf.Replace('.', ' ').Replace('-', ' ')), _config["API:token"]);
        }

        public async Task<Compra> InsertAsync(Compra entity, string cpf)
        {
            entity.Revendedor = await _revendedorRepository.FindAsync(cpf);
            if(entity.Revendedor == null)
            {
                throw new NotFoundException("Revendedor não existe!");
            }
            else if(entity.Revendedor.CPF == _config["Rev:cpf"])
            {
                entity.Aprovar();
            }

            return await _repository.InsertAsync(entity);
        }

        public async Task<ICollection<Compra>> ListAsync()
        {
            return await _repository.ListAllAsync();
        }
    }
}
