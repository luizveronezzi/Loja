using Microsoft.AspNetCore.Mvc;
using Data.Repository;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Controllers
{
    public class FinanceiroController : Controller
    {
        public ActionResult Fluxo_Caixa(string mesAtual)
        {
            if (mesAtual == null)
            {
                DateTime dataAtual = DateTime.Now;
                mesAtual = dataAtual.ToString("MM/yyyy");
            }
            var FinRep = new FinanceiroRepository();
            var retorno = FinRep.Movimento_Mensal(mesAtual);
            ViewBag.MesAtual = mesAtual;

            return View("MovimentoFinanceiro", retorno);
        }

        public ActionResult Consulta_Financeiro(int id)
        {
            var FinRep = new FinanceiroRepository();
            var retorno = FinRep.Consulta_Financeiro(id);

            return PartialView("_FichaFinanceiro", retorno);
        }
    }
}

