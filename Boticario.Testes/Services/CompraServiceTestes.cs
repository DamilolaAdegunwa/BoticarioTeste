using Boticario.Domain.Entities;
using Boticario.Domain.Interfaces.Repositories;
using Boticario.Domain.Interfaces.Services;
using Boticario.Infraestructure;
using Boticario.Infraestructure.Exceptions;
using Boticario.Infraestructure.Repositories;
using Boticario.Service.Services;
using Boticario.Testes.Util;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Boticario.Testes.Services
{
    public class CompraServiceTestes
    {
        private readonly ICompraRepository _repository;
        private readonly IRevendedorRepository _revendedorRepository;
        private readonly ICompraService _service;
        private readonly IConfiguration _configuration;
        private readonly IRevendedorService _revendedorService;
        private readonly BoticarioContext _context;

        public CompraServiceTestes()
        {
            _context = InMemoryContextFactory.Create();
            _revendedorRepository = new RevendedorRepository(_context);
            _revendedorService = new RevendedorService(_revendedorRepository, null);
            _configuration = InMemoryContextFactory.CreateConfiguration();
            _repository = new CompraRepository(_context);
            _service = new CompraService(_configuration, _repository, _revendedorRepository, null);
        }

        [Fact]
        public async void ShouldGetCashBackAsync()
        {
            string status = "200";
            var command = await _service.GetCashBackAsync("12312312323");
            Assert.NotNull(command);
            Assert.Equal(command.StatusCode, status);
        }

        [Fact]
        public async void ShouldInsertWithSucess()
        {
            var revendedor = new Revendedor()
            {
                NomeCompleto = "Nome",
                CPF = "111.222.333-00",
                Email = "email@email.com",
                Senha = "1234",
            };
            await _revendedorService.InsertAsync(revendedor);
            var compra = new Compra(1, 1200);
            var command = await _service.InsertAsync(compra, revendedor.CPF);

            Assert.NotNull(command);
            var compraBd = await _repository.FindAsync(command.Id);
            Assert.NotNull(compraBd);
        }

        [Fact]
        public async void ShouldNotInsertWhenNotExistCpf()
        {
            var revendedor = new Revendedor()
            {
                NomeCompleto = "Nome",
                CPF = "045.441.233-98",
                Email = "email@email.com",
                Senha = "1234",
            };
            await _revendedorService.InsertAsync(revendedor);
            var compra = new Compra(1, 1200);

            Func<Task> serviceCall = async () => { await _service.InsertAsync(compra, "045.441.233-00"); };
            serviceCall.Should().Throw<NotFoundException>();
        }
    }
}
