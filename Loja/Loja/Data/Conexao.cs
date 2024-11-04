using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
namespace Data
{
    public static class Conexao
    {
        public static string Host { get; set; }
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public static string Database { get; set; }
        
        public static string RetornaStringConexao()
        {
            string retornoConexao;

            Conexao.Host = "localhost";
            Conexao.UserName = "loja";
            Conexao.Password = "HortGard1261";
            Conexao.Database = "loja";

            retornoConexao = String.Format("Server={0};Port=3306;Database={1};Uid={2};Pwd={3};", Conexao.Host, Conexao.Database, Conexao.UserName, Conexao.Password);

            return retornoConexao;
        }
    }

}
