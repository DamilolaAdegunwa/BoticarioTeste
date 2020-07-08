using System;
using System.Collections.Generic;
using System.Text;

namespace Boticario.Domain.Results
{
    public class Resultado
    {
        public string StatusCode { get; set; }
        public ResultadoBody Body { get; set; }
    }
    public class ResultadoBody
    {
        public string Credit { get; set; }
    }
}
