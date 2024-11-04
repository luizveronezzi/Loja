using System.ComponentModel.DataAnnotations;


namespace Data.Models
{
    public class ProdutoModel
    {
        public int Id { get; set; }
        [Display(Name="Referência")]
        [Required(ErrorMessage = "Referência Deve Ser Informada")]
        public string Referencia { get; set; }
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Descrição do Produto Deve Ser Informada")]
        public string Descricao { get; set; }
    }
}
