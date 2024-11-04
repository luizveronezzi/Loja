using Data.Models;
using Data.Repository;
using Loja.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Loja.Controllers
{
    public class GradeController : Controller
    {
        #region Grade
        [HttpGet]
        public IActionResult Grade()
        {
            return View();
        }

        public IActionResult InsereGrade(GradeModel model)
        {
            if (!ModelState.IsValid)
            {
                string mensagem = Geral.GetErros(ModelState);
                return Json(new MensagemModel { Message = mensagem, Success = false });
            }
            var GradeRep = new GradeRepository();
            return (GradeRep.InserirGrade(model.IdProd, model.Tamanho,model.Padrao,model.Cor1, Convert.ToInt32(model.Cor2),Convert.ToInt32(model.Cor3)) == 0)
                ? Json(new MensagemModel { Message = "Grade Já Cadastrada", Success = false })
                : Json(new MensagemModel { Message = "Grade Cadastrada Com Sucesso !!", Success = true });
        }

        public IActionResult AlterarGrade(GradeModel model)
        {
            if (!ModelState.IsValid)
            {
                string mensagem = Geral.GetErros(ModelState);
                return Json(new MensagemModel { Message = mensagem, Success = false });
            }
            var GradeRep = new GradeRepository();
            return !GradeRep.AlterarGrade(model)
                ? Json(new MensagemModel { Message = "Grade Já Cadastrada", Success = false })
                : Json(new MensagemModel { Message = "Grade Alterada Com Sucesso !!", Success = true });
        }

        public IActionResult ExcluirGrade(int id)
        {
            var GradeRep = new GradeRepository();
            GradeRep.ExcluirGrade(id);
            return Json(new MensagemModel { Message = "Grade Excluida Com Sucesso !!", Success = true });
        }

        public IActionResult PesquisaGradeId(int id, string tipoRetorno, string acao)
        {
            var GradeRep = new GradeRepository();
            GradeModel result = GradeRep.ConsultaGradePorId(id, acao);
            if (tipoRetorno == "View")
            {
                return PartialView("_VerGrade", result);
            }
            else
            {
                return Json(result);
            }
        }

        [HttpGet]
        public IActionResult GridGrade(int Id)
        {
            var GradeRep = new GradeRepository();
            List<GradeDetalheModel> result = GradeRep.ListaGradePorId(Id);
            return PartialView("_ListaGrade", result);
        }
        #endregion
    }
}
