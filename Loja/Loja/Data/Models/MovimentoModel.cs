using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Models
{
    public class MovimentoModel
    {
        public int Id { get; set; }
        [Display(Name = "Documento")]
        public int Nrdocumento { get; set; }
        public int Parcela { get; set; }
        [Display(Name = "Código")]
        public int Idparceiro { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Dt. Vencto")]
        public DateTime Datavenc { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Dt. Pagto")]
        public DateTime? Datapagto { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Display(Name = "Vlr. Parcela")]
        public long Valorparc { get; set; }
        public long Saldo { get; set; }
        [Display(Name = "Vlr. Pago")]
        public long Valorpago { get; set; }
        [Display(Name = "Acrescimo")]
        public long Acrescimo { get; set; }
        public long Desconto { get; set; }
        [Display(Name = "Razão Social")]
        public string Nome { get; set; }
        public int Desprec { get; set; }
        public string D_C { get; set; }
        public string[] Desprecd = new[] { "Receita", "Despesa" };
    }

}

