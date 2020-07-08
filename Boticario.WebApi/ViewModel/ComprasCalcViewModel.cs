using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boticario.WebApi.ViewModel
{
    public class ComprasCalcViewModel
    {
        public int PorcentagemCashBack { get; set; }
        public decimal ValorCashBack { get; set; }
        public int Codigo { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string CpfRevendedor { get; set; }
    }
}
