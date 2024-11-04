using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Loja.Geral
{
    public class Geral
    {
        public static string GetErros(ModelStateDictionary modelvalid)
        {
            var query = from state in modelvalid.Values
                        from error in state.Errors
                        select error.ErrorMessage;

            var errorList = query.ToList();
            string mensagem = " ";
            foreach (var item in errorList)
            {
                mensagem += item + "<br>";
            }
            return mensagem;
        }

        public static IEnumerable<SelectListItem> GetEstados()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
            new SelectListItem{Text = "Acre", Value = "AC"},
            new SelectListItem{Text = "Alagoas", Value = "AL"},
            new SelectListItem{Text = "Amapá", Value = "AP"},
            new SelectListItem{Text = "Amazonas", Value = "AM"},
            new SelectListItem{Text = "Bahia", Value = "BA"},
            new SelectListItem{Text = "Ceará", Value = "CE"},
            new SelectListItem{Text = "Distrito Federal", Value = "DF"},
            new SelectListItem{Text = "Espírito Santo", Value = "ES"},
            new SelectListItem{Text = "Goiás", Value = "GO"},
            new SelectListItem{Text = "Maranhão", Value = "MA"},
            new SelectListItem{Text = "Mato Grosso", Value = "MT"},
            new SelectListItem{Text = "Mato Grosso do Sul", Value = "MS"},
            new SelectListItem{Text = "Minas Gerais", Value = "MG"},
            new SelectListItem{Text = "Pará", Value = "PA"},
            new SelectListItem{Text = "Paraíba", Value = "PB"},
            new SelectListItem{Text = "Paraná", Value = "PR"},
            new SelectListItem{Text = "Pernambuco", Value = "PE"},
            new SelectListItem{Text = "Piauí", Value = "PI"},
            new SelectListItem{Text = "Rio de Janeiro", Value = "RJ"},
            new SelectListItem{Text = "Rio Grande do Norte", Value = "RN"},
            new SelectListItem{Text = "Rio Grande do Sul", Value = "RS"},
            new SelectListItem{Text = "Rondônia", Value = "RO"},
            new SelectListItem{Text = "Roraima", Value = "RR"},
            new SelectListItem{Text = "Santa Catarina", Value = "SC"},
            new SelectListItem{Text = "São Paulo", Value = "SP"},
            new SelectListItem{Text = "Sergipe", Value = "SE"},
            new SelectListItem{Text = "Tocantins", Value = "TO"}
            };
            return items;
        }

    }
}
