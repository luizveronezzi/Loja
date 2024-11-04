using Dapper;
using Data.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Data.Repository
{
    public class ProdutoRepository
    {
        public ProdutoModel ConsultaProdutoPorId(int id)
        {
            string stringconexao = Conexao.RetornaStringConexao();
            ProdutoModel retorno = new ProdutoModel();

            var parametros = new DynamicParameters();
            parametros.Add("@id", id, DbType.Int32, direction: ParameterDirection.Input);

            var conn = new MySqlConnection(stringconexao);
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                retorno = conn.Query<ProdutoModel>("Select id,referencia,descricao from produtos where id = @id", parametros).FirstOrDefault();
            }
            conn.Close();

            return retorno;
        }

        public List<ProdutoModel> ConsultaTodosProduto()
        {
            string stringconexao = Conexao.RetornaStringConexao();
            List<ProdutoModel> retorno = new List<ProdutoModel>();

            var conn = new MySqlConnection(stringconexao);
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                retorno = conn.Query<ProdutoModel>("Select id,referencia,descricao from produtos").ToList();
            }
            conn.Close();

            return retorno;
        }

        public int InserirProduto(string referencia, string descricao)
        {
            int id = 0;
            string stringconexao = Conexao.RetornaStringConexao();

            var produto = new ProdutoRepository();
            ProdutoModel item = produto.ConsultaProdutoPorReferencia(referencia.ToUpper());
            if (item == null)
            {
                var parametros = new DynamicParameters();
                parametros.Add("@referencia", referencia.ToUpper(), DbType.String, direction: ParameterDirection.Input);
                parametros.Add("@descricao", descricao.ToUpper(), DbType.String, direction: ParameterDirection.Input);

                var conn = new MySqlConnection(stringconexao);
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    id = Convert.ToInt32(conn.ExecuteScalar("Insert into produtos (referencia,descricao) values (@referencia,@descricao); Select Last_Insert_Id();", parametros));
                }
                conn.Close();
            }
            return id;
        }
        public void ExcluirProduto(int id)
        {
            string stringconexao = Conexao.RetornaStringConexao();

            var parametros = new DynamicParameters();
            parametros.Add("@id", id, DbType.Int32, direction: ParameterDirection.Input);

            var conn = new MySqlConnection(stringconexao);
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                conn.Execute("Delete from produtos where id = @id", parametros);
            }
            conn.Close();

            return;
        }
        public bool AlterarProduto(ProdutoModel produtoModel)
        {
            string stringconexao = Conexao.RetornaStringConexao();
            bool retorno = false;
            int idanteror = produtoModel.Id;

            var produto = new ProdutoRepository();
            ProdutoModel item = produto.ConsultaProdutoPorReferencia(produtoModel.Referencia.ToUpper());
            if (item == null || item.Id == idanteror)
            {
                var parametros = new DynamicParameters();
                parametros.Add("@id", produtoModel.Id, DbType.Int32, direction: ParameterDirection.Input);
                parametros.Add("@descricao", produtoModel.Descricao.ToUpper(), DbType.String, direction: ParameterDirection.Input);
                parametros.Add("@referencia", produtoModel.Referencia.ToUpper(), DbType.String, direction: ParameterDirection.Input);

                var conn = new MySqlConnection(stringconexao);
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Execute("Update produtos set referencia = @referencia, descricao = @descricao where id = @id", parametros);
                    retorno = true;
                }
                conn.Close();
            }
            return retorno;
        }

        public ProdutoModel ConsultaProdutoPorReferencia(string referencia)
        {
            string stringconexao = Conexao.RetornaStringConexao();
            ProdutoModel retorno = new ProdutoModel();

            var parametros = new DynamicParameters();
            parametros.Add("@referencia", referencia, DbType.String, direction: ParameterDirection.Input);

            var conn = new MySqlConnection(stringconexao);
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                retorno = conn.Query<ProdutoModel>("Select id,referencia,descricao from produtos where referencia = @referencia", parametros).FirstOrDefault();
            }
            conn.Close();

            return retorno;
        }
    }
}
