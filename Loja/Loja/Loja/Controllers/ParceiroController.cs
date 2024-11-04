using Data.Models;
using Data.Repository;
using Loja.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Loja.Controllers
{
    public class ParceiroController : Controller
    {
        public IActionResult Parceiro()
        {
            return View();
        }
        public IActionResult InsereParceiro(ParceiroModel model)
        {
            if (!ModelState.IsValid)
            {
                string mensagem = Geral.GetErros(ModelState);
                return Json(new MensagemModel { Message = mensagem, Success = false });
            }
            var ParcRep = new ParceiroRepository();
            return (ParcRep.InserirParceiro(model) == 0)
                ? Json(new MensagemModel { Message = "Parceiro Já Cadastrado", Success = false })
                : Json(new MensagemModel { Message = "Parceiro Cadastrado Com Sucesso !!", Success = true });
        }

        public IActionResult AlterarParceiro(ParceiroModel model)
        {
            if (!ModelState.IsValid)
            {
                string mensagem = Geral.GetErros(ModelState);
                return Json(new MensagemModel { Message = mensagem, Success = false });

            }
            var ParcRep = new ParceiroRepository();
            return !ParcRep.AlterarParceiro(model)
                ? Json(new MensagemModel { Message = "Parceiro Já Cadastrado", Success = false })
                : Json(new MensagemModel { Message = "Parceiro Alterado Com Sucesso !!", Success = true });
        }

        public IActionResult ExcluirParceiro(int id)
        {
            var ParcRep = new ParceiroRepository();
            ParcRep.ExcluirParceiro(id);
            return Json(new MensagemModel { Message = "Parceiro Excluido Com Sucesso !!", Success = true });
        }

        [HttpGet]
        public IActionResult GridParceiro()
        {
            var ParcRep = new ParceiroRepository();
            List<ParceiroModel> result = ParcRep.ConsultaTodosParceiro();
            return PartialView("_ListaParceiros", result);
        }

        //public IActionResult PesquisaParceiroReferencia(string referencia)
        //{
        //    var ParcRep = new ParceiroRepository();
        //    var result = ParcRep.ConsultaParceiroPorReferencia(referencia);
        //    return Json(result);
        //}

        public IActionResult PesquisaParceiroId(int id, string tipoRetorno)
        {
            var ParcRep = new ParceiroRepository();
            ParceiroModel result = ParcRep.ConsultaParceiroPorId(id);
            if (tipoRetorno == "View")
            {
                return PartialView("_VerParceiro", result);
            }
            else
            {
                return Json(result);
            }
        }

    }
}
