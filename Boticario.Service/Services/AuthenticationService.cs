using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Boticario.Domain.Interfaces;
using Boticario.Domain.Interfaces.Repositories;
using Boticario.Domain.Interfaces.Services;

namespace Boticario.Service.Services
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private readonly IRevendedorRepository _repository;
        public AuthenticationService(IRevendedorRepository repository,
                            IUnitOfWorkFactory unitOfWorkFactory) : base(unitOfWorkFactory)
        {
            _repository = repository;
        }
        public bool ValidateLogin(string email, string senha)
        {
            return _repository.ValidateLogin(email, senha);
        }
    }
}
