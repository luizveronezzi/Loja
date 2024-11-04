using Dapper;
using Data.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Data.Repository
{
    public class FinanceiroRepository
    {
        public List<MovimentoModel> Movimento_Mensal(string mesano)
        {
            string csql = @"
                            SELECT 
                                   DATAVENC,
                                   DESCRICAO,
                                   D_C,
                                   VALORPARC,
	                               SUM(VALORPARC) OVER (ORDER BY DATAVENC ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) SALDO,
                                   ID,
	                               DATAPAGTO,
                                   DESPREC 
                            FROM (   
                                   SELECT 
	                                    DATE_SUB(STR_TO_DATE(CONCAT('01/',@mesano),'%d/%m/%Y'),INTERVAL 1 DAY) DATAVENC,
	                                    'SALDO ANTERIOR' DESCRICAO,
	                                    'S' D_C,
	                                    SUM(CASE WHEN DESPREC = 2 THEN -VALORPARC ELSE VALORPARC END) VALORPARC,
                                    	0 ID,
	                                    DATE_SUB(STR_TO_DATE(CONCAT('01/',@mesano),'%d/%m/%Y'),INTERVAL 1 DAY) DATAPAGTO,
                                        0 DESPREC
                                   FROM 
	                                    FATURAS 
                                   WHERE 
	                                    DATE_FORMAT(DATAVENC,'%m/%Y') < @mesano
                            UNION ALL 
                                   SELECT 
                                        DATAVENC , 
                                        DESCRICAO,
                                        CASE WHEN DESPREC = 1 THEN 'C'ELSE 'D' END D_C ,
                                        CASE WHEN DESPREC = 2 THEN -VALORPARC ELSE VALORPARC END VALORPARC,
                                        ID,
                                        DATAPAGTO,
                                        DESPREC
                                    FROM
                                        FATURAS 
                                    WHERE 
	                                    DATE_FORMAT(DATAVENC,'%m/%Y') = @mesano
                                    ORDER BY
                                        DATAVENC 
                                  ) SALDOS;
                            ";

            List<MovimentoModel> retorno = new List<MovimentoModel>();
            var parametros = new DynamicParameters();
            parametros.Add("@mesano", mesano, DbType.String, direction: ParameterDirection.Input);
            string stringconexao = Conexao.RetornaStringConexao();

            var conn = new MySqlConnection(stringconexao);
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {

                retorno = conn.Query<MovimentoModel>(csql, parametros).ToList();
            }
            conn.Close();

            return retorno;
        }

        public MovimentoModel Consulta_Financeiro(int id)
        {
            string csql = @"
                            SELECT 
                                F.ID, 
                                F.NRDOCUMENTO, 
                                F.PARCELA, 
                                F.DATAVENC, 
                                F.DATAPAGTO, 
                                F.VALORPARC, 
                                F.DESCONTO, 
                                F.ACRESCIMO, 
                                F.VALORPAGO, 
                                F.IDPARCEIRO, 
                                CASE WHEN F.DESPREC = 1 THEN 'C'ELSE 'D' END D_C,
                                F.DESCRICAO,
                                P.NOME 
                            FROM 
                                FATURAS f
                            INNER JOIN 
                                PARCEIROS P ON P.ID  = F.IDPARCEIRO 
                            WHERE 
                                F.ID = @id;
                            ";
            MovimentoModel retorno = new MovimentoModel();
            var parametros = new DynamicParameters();
            parametros.Add("@id", id, DbType.Int64, direction: ParameterDirection.Input);
            string stringconexao = Conexao.RetornaStringConexao();

            var conn = new MySqlConnection(stringconexao);
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {

                retorno = conn.Query<MovimentoModel>(csql, parametros).FirstOrDefault();
            }
            conn.Close();

            return retorno;
        }

    }
}
