using System;
using System.Collections.Generic;
using System.Text;

namespace Boticario.Domain.Entities
{
    public class Revendedor : EntityBase
    {
        public Revendedor()
        {
            this.Compras = new HashSet<Compra>();
        }
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public ICollection<Compra> Compras { get; set; }
    }
}
