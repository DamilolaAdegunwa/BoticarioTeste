using Boticario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Boticario.Testes.Domain
{
    public class CompraTestes
    {
        [Fact]
        public void ShouldFillCodeAndValue()
        {
            int codigo = 1;
            decimal valor = 100;
            var compra = new Compra(codigo, valor);

            Assert.Equal(compra.Codigo, codigo);
            Assert.Equal(compra.Valor, valor);
        }

        [Fact]
        public void ShouldChangeStatusToAprove()
        {
            int codigo = 1;
            decimal valor = 100;
            var statusEmValidacao = StatusCompra.EmValidacao;
            var statusAprovado = StatusCompra.Aprovado;
            var compra = new Compra(codigo, valor);
            Assert.Equal(compra.Status, statusEmValidacao);
            compra.Aprovar();
            Assert.Equal(compra.Status, statusAprovado);
        }

        [Fact]
        public void ShouldCalculatePercentagem()
        {
            int porcentagem10 = 10;
            int porcentagem15 = 15;
            int porcentagem20 = 20;
            var compra10 = new Compra(1, 999);
            var compra15 = new Compra(2, 1200);
            var compra20 = new Compra(3, 1800);

            Assert.Equal(compra10.PorcentagemAplicada(), porcentagem10);
            Assert.Equal(compra15.PorcentagemAplicada(), porcentagem15);
            Assert.Equal(compra20.PorcentagemAplicada(), porcentagem20);
        }

        [Fact]
        public void ShouldCalculateCashBack()
        {
            int cash1 = 50;
            int cash2 = 180;
            int cash3 = 360;
            var compra10 = new Compra(1, 500);
            var compra15 = new Compra(2, 1200);
            var compra20 = new Compra(3, 1800);

            Assert.Equal(compra10.CalcularCashBack(), cash1);
            Assert.Equal(compra15.CalcularCashBack(), cash2);
            Assert.Equal(compra20.CalcularCashBack(), cash3);
        }
    }
}
