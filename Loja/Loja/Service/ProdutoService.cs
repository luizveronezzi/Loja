using Data.Repository;
using Loja.Models;

namespace Service
{
    public class ProdutoService
    {
        public bool InsereProduto(string referencia, string descricao)
        {
            bool retorno;
            var produto = new ProdutoRepository();
            ProdutoModel item = produto.ConsultaProdutoPorReferencia(referencia);
            if (item.Id == 0)
            {
                int newId = produto.InserirProduto(referencia, descricao);
                retorno = newId != 0;
            }
            else
            {
                retorno = false;
            }
            return retorno;
        }
    }
}