using Dapper;
using Data.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Data.Repository
{
    public class OpcoesRepository
    {
        public List<OpcoesModel> RetornaOpcoes(string titulo)
        {
            string stringconexao = Conexao.RetornaStringConexao();
            List<OpcoesModel> retorno = new List<OpcoesModel>();

            var parametros = new DynamicParameters();
            parametros.Add("@titulo", titulo, DbType.String, direction: ParameterDirection.Input);

            var conn = new MySqlConnection(stringconexao);
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                retorno = conn.Query<OpcoesModel>("Select id,descricao from opcoes where idcabopcoes = (select id from cabopcoes where titulo = @titulo) order by ordem",parametros).ToList();
            }
            conn.Close();

            return retorno;
        }
    }
}
