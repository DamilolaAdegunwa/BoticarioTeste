using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Boticario.WebApi.ViewModel
{
    public class ComprasViewModel
    {
        [Required(ErrorMessage = "Código obrigatório")]
        public int Codigo { get; set; }
        [Required(ErrorMessage = "Valor obrigatório")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "Data obrigatório")]
        public DateTime Data { get; set; }
        [Required(ErrorMessage = "Cpf revendedor obrigatório")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}\-\d{2}$", ErrorMessage = "Você deve digitar um CPF com formato válido [000.000.000-00]")]
        public string CpfRevendedor { get; set; }
    }
}
