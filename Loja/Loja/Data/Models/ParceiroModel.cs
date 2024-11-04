using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Models
{
    public class ParceiroModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Cargo Deve Ser Infomado")]
        [RegularExpression(@"[1-9]{1}.*", ErrorMessage = "Cargo Deve Ser Infomado")]
        [Display(Name = "Cargo")]
        public int Idcargo { get; set; }
        [Required(ErrorMessage = "Tipo Parceiro Deve Ser Infomado")]
        [RegularExpression(@"[1-9]{1}.*", ErrorMessage = "Tipo Parceiro Deve Ser Infomado")]
        [Display(Name = "Tipo de Parceiro")]
        public int Idtipo { get; set; }
        [Display(Name = "CNPJ ou CPF")]
        public string Cnpj_cpf { get; set; }
        [Required(ErrorMessage = "Razão Social Deve de Ser Infomada")]
        [Display(Name = "Razão Social")]
        public string Nome { get; set; }
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        [Display(Name = "UF")] 
        public string Estado { get; set; }
        public string Cep { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Site { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        [Display(Name = "Cargo")]
        public string descrCargo { get; set; }
        [Display(Name = "Tipo do Parceiro")]
        public string descrTipoParc { get; set; }
        public List<OpcoesModel> ListaCargo { get; set; }
        public List<OpcoesModel> ListaTipoParc { get; set; }
    }
}

