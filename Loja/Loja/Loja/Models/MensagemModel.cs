using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Models
{
    public class MensagemModel
    {
        public string Message { set; get; }
        public bool Success { set; get; }
        public tipoMessage Tipo { get; set; }
        public string Titulo { get; set; }
    }
}
