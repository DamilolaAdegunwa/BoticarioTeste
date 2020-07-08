using Boticario.Domain.Entities;
using Boticario.Domain.Interfaces.Repositories;
using Boticario.Domain.Interfaces.Services;
using Boticario.Infraestructure;
using Boticario.Infraestructure.Repositories;
using Boticario.Service.Services;
using Boticario.Testes.Util;
using Xunit;

namespace Boticario.Testes.Services
{
    public class RevendedorServiceTestes
    {
        private readonly IRevendedorRepository _repository;
        private readonly IRevendedorService _service;

        private readonly BoticarioContext _context;

        public RevendedorServiceTestes()
        {
            _context = InMemoryContextFactory.Create();
            _repository = new RevendedorRepository(_context);
            _service = new RevendedorService(_repository, null);
        }


        [Fact]
        public async void ShouldInsertRevendedor()
        {
            var revendedor = new Revendedor()
            {
                NomeCompleto = "Nome",
                CPF = "111.222.333-00",                
                Email = "email@email.com",
                Senha = "1234"
            };
            var command = await _service.InsertAsync(revendedor);

            Assert.NotNull(command);
            var resultado = await _repository.FindAsync(command.Id);
            Assert.NotNull(resultado);
        }
    }
}
