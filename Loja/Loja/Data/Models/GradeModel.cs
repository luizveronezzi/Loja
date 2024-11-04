using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Models
{
    public class GradeModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Referência Deve Ser Informada")]
        public int IdProd { get; set; }
        [Required(ErrorMessage = "Tamanho Deve Ser Informado")]
        [RegularExpression(@"[1-9]{1}.*", ErrorMessage = "Tamanho Deve Ser Informado")]
        public int Tamanho { get; set; }
        [Display(Name = "Padrão")]
        [Required(ErrorMessage = "Padrão Deve Ser Informado")]
        [RegularExpression(@"[1-9]{1}.*", ErrorMessage = "Padrão Deve Ser Informado")]
        public int Padrao { get; set; }
        [Required(ErrorMessage = "Cor Deve Ser Informado")]
        [RegularExpression(@"[1-9]{1}.*", ErrorMessage = "Cor Deve Ser Informada")]
        public int Cor1 { get; set; }
        public int? Cor2 { get; set; }
        public int? Cor3 { get; set; }
        public string Foto { get; set; }
        public GradeDetalheModel GradeDetalhe { get; set; }
        public List<OpcoesModel> ListaCores { set; get; }
        public List<OpcoesModel> ListaTamanho { set; get; }
        public List<OpcoesModel> ListaPadrao { set; get; }
        public List<GradeDetalheModel> ListaGrade { set; get; }
        public List<ProdutoModel> ListaProduto { get; set; }
    }

    public class GradeDetalheModel
    {
        public int Id { set; get; }
        [Display(Name = "Referência")]
        [Required(ErrorMessage = "Referência Deve Ser Informada")]
        public string Referencia { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public string Tamanho { get; set; }
        public string Padrao { get; set; }
        public string Cor1 { get; set; }
        public string Cor2 { get; set; }
        public string Cor3 { get; set; }
        public string ReferenciaGrade { get; set; }
    }

}

