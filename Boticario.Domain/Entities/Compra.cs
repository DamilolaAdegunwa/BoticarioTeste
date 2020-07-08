using System;
using System.Collections.Generic;
using System.Text;

namespace Boticario.Domain.Entities
{
    public class Compra : EntityBase
    {
        public Compra(int codigo, decimal valor)
        {
            this.Codigo = codigo;
            this.Valor = valor;
            this.Data = DateTime.Now;
            this.Status = StatusCompra.EmValidacao;
        }
        public int Codigo { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public StatusCompra Status { get; set; }
        public int Revendedor_Id { get; set; }
        public Revendedor Revendedor { get; set; }

        public void Aprovar()
        {
            this.Status = StatusCompra.Aprovado;
        }
        public decimal CalcularCashBack()
        {
            if(this.Valor < 1000)
            {
                return 0.10M * this.Valor;
            }
            else if (this.Valor >= 1000 && this.Valor < 1500)
            {
                return 0.15M * this.Valor;
            }
            else if (this.Valor > 1500)
            {
                return 0.20M * this.Valor;
            }
            return 0;
        }

        public int PorcentagemAplicada()
        {
            if (this.Valor < 1000)
            {
                return 10;
            }
            else if (this.Valor >= 1000 && this.Valor < 1500)
            {
                return 15;
            }
            else if (this.Valor > 1500)
            {
                return 20;
            }
            return 0;
        }


    }
    public enum StatusCompra
    {
        EmValidacao,
        Aprovado
    }
}
