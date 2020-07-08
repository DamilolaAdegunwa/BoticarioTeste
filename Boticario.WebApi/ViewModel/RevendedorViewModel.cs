using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Boticario.WebApi.ViewModel
{
    public class RevendedorViewModel
    {
        [Required(ErrorMessage = "Nome completo obrigatório")]
        public string NomeCompleto { get; set; }
        [Required]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}\-\d{2}$", ErrorMessage = "Você deve digitar um CPF com formato válido [000.000.000-00]")]
        public string CPF { get; set; }
        [Required]
        [RegularExpression(@"^[a-z0-9.]+@[a-z0-9]+\.[a-z]+(\.[a-z]+)?$", ErrorMessage = "Você deve digitar um Email válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha obrigatório")]
        public string Senha { get; set; }
    }
}
