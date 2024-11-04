using Dapper;
using Data.Models;
using Data.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Data.Repository
{
    public class GradeRepository
    {
        public GradeModel ConsultaGradePorId(int id, string acao)
        {
            string query = "";
            string stringconexao = Conexao.RetornaStringConexao();
            GradeModel retorno = new GradeModel();
            retorno.GradeDetalhe = new GradeDetalheModel();
            retorno.ListaGrade = new List<GradeDetalheModel>();
            retorno.ListaProduto = new List<ProdutoModel>();
            if (acao == "Alteracao")
            {
                query = @"
                             select Id,IdProd,Tamanho,Padrao,Cor1,Cor2,Cor3,Foto from Grade where Id = @id;
                             select g.Id,p.referencia,p.descricao,
                             (select descricao from opcoes where id = g.Tamanho ) tamanho,
                             (select descricao from opcoes where id = g.Padrao) padrao,
                             (select descricao from opcoes where id = g.Cor1) cor1,
                             (select descricao from opcoes where id = g.Cor2) cor2,
                             (select descricao from opcoes where id = g.Cor3) cor3
                             from produtos p
                             inner join grade g on g.IDPROD = p.ID 
                             where g.Id = @id;
                            ";
            }
            if (acao == "Inserir")
            {
                query = @"
                             select 0 Id,Id IdProd,null Tamanho,null Padrao,null Cor1,null Cor2,null Cor3,null Foto from Produtos where Id = @id;
                             select 0,referencia,descricao,null Tamanho,null Padrao,null Cor1,null Cor2,null Cor3,null Foto from Produtos where Id = @id;
                            ";

            }
            var parametros = new DynamicParameters();
            parametros.Add("@id", id, DbType.Int32, direction: ParameterDirection.Input);

            var conn = new MySqlConnection(stringconexao);
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                using (var result = conn.QueryMultiple(query, parametros))
                {
                    try
                    {
                        retorno = result.Read<GradeModel>().Single();
                        retorno.GradeDetalhe = result.Read<GradeDetalheModel>().Single();
                    }
                    catch (InvalidOperationException error)
                    {
                    }
                }
                var OpRep = new OpcoesRepository();
                var PrRep = new ProdutoRepository();
                retorno.ListaCores = OpRep.RetornaOpcoes("CORES");
                retorno.ListaPadrao = OpRep.RetornaOpcoes("PADRAO");
                retorno.ListaTamanho = OpRep.RetornaOpcoes("TAMANHO");
                retorno.ListaProduto = PrRep.ConsultaTodosProduto();
            }
            conn.Close();

            return retorno;
        }

        public List<GradeDetalheModel> ListaGradePorId(int id)
        {
            string stringconexao = Conexao.RetornaStringConexao();
            List<GradeDetalheModel> retorno = new List<GradeDetalheModel>();
            string query = @"
                            select g.id, 
                            (select descricao from opcoes where id = g.Tamanho) tamanho,
                            (select descricao from opcoes where id = g.Padrao) padrao,
                            (select descricao from opcoes where id = g.Cor1) cor1,
                            (select descricao from opcoes where id = g.Cor2) cor2,
                            (select descricao from opcoes where id = g.Cor3) cor3
                            from produtos p
                            inner join grade g on g.IDPROD = p.ID
                            where g.IdProd = @id;
                            ";

            var parametros = new DynamicParameters();
            parametros.Add("@id", id, DbType.Int32, direction: ParameterDirection.Input);

            var conn = new MySqlConnection(stringconexao);
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                retorno = conn.Query<GradeDetalheModel>(query, parametros).ToList();
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

        public int InserirGrade(int idprod, int tamanho, int padrao, int cor1, int cor2, int cor3)
        {
            int id = 0;
            string stringconexao = Conexao.RetornaStringConexao();


            int result = ConsultaGradeExistente(idprod, tamanho, padrao, cor1, cor2, cor3);
            if (result == 0)
            {
                var parametros = new DynamicParameters();
                parametros.Add("@idprod", idprod, DbType.Int64, direction: ParameterDirection.Input);
                parametros.Add("@tamanho", tamanho, DbType.Int64, direction: ParameterDirection.Input);
                parametros.Add("@padrao", padrao, DbType.Int64, direction: ParameterDirection.Input);
                parametros.Add("@cor1", cor1, DbType.Int64, direction: ParameterDirection.Input);
                parametros.Add("@cor2", cor2, DbType.Int64, direction: ParameterDirection.Input);
                parametros.Add("@cor3", cor3, DbType.Int64, direction: ParameterDirection.Input);


                var conn = new MySqlConnection(stringconexao);
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    id = Convert.ToInt32(conn.ExecuteScalar("Insert into grade (idprod,tamanho,padrao,cor1,cor2,cor3) values (@idprod,@tamanho,@padrao,@cor1,@cor2,@cor3); Select Last_Insert_Id();", parametros));
                }
                conn.Close();
            }
            return id;
        }

        public int ConsultaGradeExistente(int idprod, int tamanho, int padrao, int cor1, int cor2, int cor3)
        {
            int result = 0;
            string stringconexao = Conexao.RetornaStringConexao();
            var parametros = new DynamicParameters();
            parametros.Add("@idprod", idprod, DbType.Int32, direction: ParameterDirection.Input);
            parametros.Add("@tamanho", tamanho, DbType.Int32, direction: ParameterDirection.Input);
            parametros.Add("@padrao", padrao, DbType.Int32, direction: ParameterDirection.Input);
            parametros.Add("@cor1", cor1, DbType.Int32, direction: ParameterDirection.Input);
            parametros.Add("@cor2", cor2, DbType.Int32, direction: ParameterDirection.Input);
            parametros.Add("@cor3", cor3, DbType.Int32, direction: ParameterDirection.Input);

            var conn = new MySqlConnection(stringconexao);
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                result = Convert.ToInt32(conn.ExecuteScalar("select count(*) from grade where idprod = @idprod and tamanho = @tamanho and padrao = @padrao and cor1 = @cor1 and ifnull(cor2,0) = @cor2 and ifnull(cor3,0) = @cor3;",parametros));
            }
            return result;
        }

        public void ExcluirGrade(int id)
        {
            string stringconexao = Conexao.RetornaStringConexao();

            var parametros = new DynamicParameters();
            parametros.Add("@id", id, DbType.Int32, direction: ParameterDirection.Input);

            var conn = new MySqlConnection(stringconexao);
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                conn.Execute("Delete from grade where id = @id", parametros);
            }
            conn.Close();

            return;
        }
        public bool AlterarGrade(GradeModel gradeModel)
        {
            string stringconexao = Conexao.RetornaStringConexao();
            bool retorno = false;

            int result = ConsultaGradeExistente(gradeModel.IdProd, gradeModel.Tamanho, gradeModel.Padrao, gradeModel.Cor1, Convert.ToInt32(gradeModel.Cor2), Convert.ToInt32(gradeModel.Cor3));
            if (result == 0)
            {
                var parametros = new DynamicParameters();
                parametros.Add("@id", gradeModel.Id, DbType.Int32, direction: ParameterDirection.Input);
                parametros.Add("@idprod", gradeModel.IdProd, DbType.Int32, direction: ParameterDirection.Input);
                parametros.Add("@tamanho", gradeModel.Tamanho, DbType.Int32, direction: ParameterDirection.Input);
                parametros.Add("@padrao", gradeModel.Padrao, DbType.Int32, direction: ParameterDirection.Input);
                parametros.Add("@cor1", gradeModel.Cor1, DbType.Int32, direction: ParameterDirection.Input);
                parametros.Add("@cor2", Convert.ToInt32(gradeModel.Cor2), DbType.Int32, direction: ParameterDirection.Input);
                parametros.Add("@cor3", Convert.ToInt32(gradeModel.Cor3), DbType.Int32, direction: ParameterDirection.Input);

                var conn = new MySqlConnection(stringconexao);
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Execute("Update grade set tamanho = @tamanho, padrao = @padrao, cor1 = @cor1, cor2 = @cor2, cor3 = @cor3 where id = @id and idprod = @idprod", parametros);
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
