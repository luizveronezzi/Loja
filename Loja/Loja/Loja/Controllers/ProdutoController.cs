using Data.Models;
using Data.Repository;
using Loja.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Loja.Controllers
{
    public class ProdutoController : Controller
    {
        #region Produtos
        [HttpGet]
        public IActionResult Produto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InsereProduto(ProdutoModel model)
        {
            if (!ModelState.IsValid)
            {
                string mensagem = Geral.GetErros(ModelState);
                return Json(new MensagemModel { Message = mensagem, Success = false });
            }
            var ProdRep = new ProdutoRepository();
            return (ProdRep.InserirProduto(model.Referencia, model.Descricao) == 0)
                ? Json(new MensagemModel { Message = "Produto Já Cadastrado", Success = false })
                : Json(new MensagemModel { Message = "Produto Cadastrado Com Sucesso !!", Success = true });
        }

        public IActionResult AlterarProduto(ProdutoModel model)
        {
            if (!ModelState.IsValid)
            {
                string mensagem = Geral.GetErros(ModelState);
                return Json(new MensagemModel { Message = mensagem, Success = false });

            }
            var ProdRep = new ProdutoRepository();
            return !ProdRep.AlterarProduto(model)
                ? Json(new MensagemModel { Message = "Produto Já Cadastrado", Success = false })
                : Json(new MensagemModel { Message = "Produto Alterado Com Sucesso !!", Success = true });
        }

        public IActionResult ExcluirProduto(int id)
        {
            var ProdRep = new ProdutoRepository();
            ProdRep.ExcluirProduto(id);
            return Json(new MensagemModel { Message = "Produto Excluido Com Sucesso !!", Success = true });
        }

        [HttpGet]
        public IActionResult GridProduto()
        {
            var ProdRep = new ProdutoRepository();
            List<ProdutoModel> result = ProdRep.ConsultaTodosProduto();
            return PartialView("_ListaProdutos", result);
        }

        public IActionResult PesquisaProdutoReferencia(string referencia)
        {
            var ProdRep = new ProdutoRepository();
            var result = ProdRep.ConsultaProdutoPorReferencia(referencia);
            return Json(result);
        }

        public IActionResult PesquisaProdutoId(int id, string tipoRetorno)
        {
            var ProdRep = new ProdutoRepository();
            ProdutoModel result = ProdRep.ConsultaProdutoPorId(id);
            if (tipoRetorno == "View")
            {
                return PartialView("_VerProduto", result);
            }
            else
            {
                return Json(result);
            }
        }
        #endregion
    }
}
