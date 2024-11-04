using Dapper;
using Data.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Data.Repository
{
    public class ParceiroRepository
    {
        public ParceiroModel ConsultaParceiroPorId(int id)
        {
            string stringconexao = Conexao.RetornaStringConexao();
            ParceiroModel retorno = new ParceiroModel();
            retorno.ListaTipoParc = new List<OpcoesModel>();
            retorno.ListaCargo = new List<OpcoesModel>();

            var parametros = new DynamicParameters();
            parametros.Add("@id", id, DbType.Int32, direction: ParameterDirection.Input);

            var conn = new MySqlConnection(stringconexao);
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                try
                {
                    retorno = conn.Query<ParceiroModel>(@"Select 
                                                            id,nome,endereco,bairro,numero,complemento,cidade,
                                                            estado,cep,cnpj_cpf,celular,email,facebook,instagram,
                                                            site,idcargo,idtipo,
                                                            (select descricao from opcoes where id = idcargo) descrCargo,
                                                            (select descricao from opcoes where id = idtipo) descrTipoParc
                                                        from 
                                                            Parceiros 
                                                        where 
                                                            id = @id"
                                                        , parametros).FirstOrDefault();
                    if (retorno == null)
                    {
                        retorno = new ParceiroModel();
                    }
                }
                catch (InvalidOperationException error)
                {
                }
                var OpRep = new OpcoesRepository();
                retorno.ListaCargo = OpRep.RetornaOpcoes("CARGO");
                retorno.ListaTipoParc = OpRep.RetornaOpcoes("TIPOPARC");
            }
            conn.Close();

            return retorno;
        }

        public List<ParceiroModel> ConsultaTodosParceiro()
        {
            string stringconexao = Conexao.RetornaStringConexao();
            List<ParceiroModel> retorno = new List<ParceiroModel>();

            var conn = new MySqlConnection(stringconexao);
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                retorno = conn.Query<ParceiroModel>(@"Select  
                                                        id,nome,endereco,bairro,numero,complemento,cidade,
                                                        estado,cep,cnpj_cpf,celular,email,facebook,instagram,
                                                        site,idcargo,idtipo, 
                                                        (select descricao from opcoes where id = idcargo) descrCargo,
                                                        (select descricao from opcoes where id = idtipo) descrTipoParc
                                                    from 
                                                        Parceiros"
                                                    ).ToList();
            }
            conn.Close();

            return retorno;
        }

        public int InserirParceiro(ParceiroModel model)
        {
            int id = 0;
            string cCep = null;
            string cCelular = null;
            string stringconexao = Conexao.RetornaStringConexao();

            string cCnpj = String.Join("", System.Text.RegularExpressions.Regex.Split(model.Cnpj_cpf, @"[^\d]"));

            if (model.Cep != null)
            {
                cCep = String.Join("", System.Text.RegularExpressions.Regex.Split(model.Cep, @"[^\d]"));
            }
            if (model.Celular != null)
            {
                cCelular = String.Join("", System.Text.RegularExpressions.Regex.Split(model.Celular, @"[^\d]"));
            }

            var parametros = new DynamicParameters();
            parametros.Add("@Nome", model.Nome.ToUpper(), DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Cnpj_cpf", cCnpj, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Endereco", model.Endereco, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Numero", model.Numero, DbType.Int32, direction: ParameterDirection.Input);
            parametros.Add("@Bairro", model.Bairro, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Cidade", model.Cidade, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Estado", model.Estado, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Cep", cCep, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Complemento", model.Complemento, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Celular", cCelular, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Site", model.Site, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Email", model.Email, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Instagram", model.Instagram, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Facebook", model.Facebook, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Idcargo", model.Idcargo, DbType.Int32, direction: ParameterDirection.Input);
            parametros.Add("@Idtipo", model.Idtipo, DbType.Int32, direction: ParameterDirection.Input);

            var conn = new MySqlConnection(stringconexao);
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                id = Convert.ToInt32(conn.ExecuteScalar(@"Insert into Parceiros 
                                                              (nome,endereco,bairro,numero,complemento,cidade,estado,cep,cnpj_cpf,celular,email,facebook,instagram,site,idcargo,idtipo) 
                                                              values 
                                                              (@Nome,@Endereco,@Bairro,@Numero,@Complemento,@Cidade,@Estado,@Cep,@Cnpj_cpf,@Celular,@Email,@Facebook,@Instagram,@Site,@Idcargo,@Idtipo); 
                                                              Select Last_Insert_Id();"
                                                          , parametros));
            }
            conn.Close();
            //  }
            return id;
        }
        public void ExcluirParceiro(int id)
        {
            string stringconexao = Conexao.RetornaStringConexao();

            var parametros = new DynamicParameters();
            parametros.Add("@id", id, DbType.Int32, direction: ParameterDirection.Input);

            var conn = new MySqlConnection(stringconexao);
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                conn.Execute("Delete from Parceiros where id = @id", parametros);
            }
            conn.Close();

            return;
        }
        public bool AlterarParceiro(ParceiroModel model)
        {
            string stringconexao = Conexao.RetornaStringConexao();
            bool retorno = false;
            int idanteror = model.Id;
            string cCep = null;
            string cCelular = null;

            string cCnpj = String.Join("", System.Text.RegularExpressions.Regex.Split(model.Cnpj_cpf, @"[^\d]"));

            if (model.Cep != null)
            {
                cCep = String.Join("", System.Text.RegularExpressions.Regex.Split(model.Cep, @"[^\d]"));
            }
            if (model.Celular != null)
            {
                cCelular = String.Join("", System.Text.RegularExpressions.Regex.Split(model.Celular, @"[^\d]"));
            }

            var parametros = new DynamicParameters();
            parametros.Add("@id", model.Id, DbType.Int32, direction: ParameterDirection.Input);
            parametros.Add("@Nome", model.Nome.ToUpper(), DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Cnpj_cpf", cCnpj, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Endereco", model.Endereco, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Numero", model.Numero, DbType.Int32, direction: ParameterDirection.Input);
            parametros.Add("@Bairro", model.Bairro, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Cidade", model.Cidade, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Estado", model.Estado, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Cep", cCep, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Complemento", model.Complemento, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Celular", cCelular, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Site", model.Site, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Email", model.Email, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Instagram", model.Instagram, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Facebook", model.Facebook, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@Idcargo", model.Idcargo, DbType.Int32, direction: ParameterDirection.Input);
            parametros.Add("@Idtipo", model.Idtipo, DbType.Int32, direction: ParameterDirection.Input);

            var conn = new MySqlConnection(stringconexao);
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                conn.Execute(@"Update Parceiros set 
                                    nome=@Nome,
                                    endereco = @Endereco,
                                    bairro = @Bairro,
                                    numero = @Numero,
                                    complemento = @Complemento,
                                    cidade = @Cidade,
                                    estado = @Estado,
                                    cep = @Cep,
                                    cnpj_cpf = @Cnpj_cpf,
                                    celular = @Celular,
                                    email = @Email,
                                    facebook = @Facebook,
                                    instagram = @Instagram,
                                    site = @Site,
                                    idcargo = @Idcargo,
                                    idtipo = @Idtipo 
                                    where id = @id"
                                , parametros);
                retorno = true;
            }
            conn.Close();
            //  }
            return retorno;
        }

    }
}
