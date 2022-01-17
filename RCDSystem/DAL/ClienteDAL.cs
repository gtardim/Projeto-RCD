using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.DAL
{
    public class ClienteDAL
    {


        private Banco b;
        internal ClienteDAL()
        {
            b = Banco.GetInstance();
        }

        internal List<Models.Cliente> TableToList(DataTable dt)
        {


            List<Models.Cliente> dados = new List<Models.Cliente>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Models.Cidade cid = new Models.Cidade();
                    int cod_cid = Convert.ToInt32(row["cidade"]);
                    cid = new DAL.CidadeDAL().ObterCidade(cod_cid);

                    Models.Cliente cli = new Models.Cliente((int)row["codigo"], row["nome"].ToString(), row["nacionalidade"].ToString(),
                                                                    row["estadocivil"].ToString(), row["RG"].ToString(), row["CPF"].ToString(),
                                                                    row["CNPJ"].ToString(), row["rua"].ToString(), row["bairro"].ToString(), cid,
                                                                    row["numero"].ToString(), row["CEP"].ToString(), row["email"].ToString(), row["contato"].ToString(),
                                                                      (int)row["tipocliente"]);

                    dados.Add(cli);
                }
                return dados;
            }
            else
                return null;


        }

        internal Boolean Gravar(Models.Cliente cli)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@nome", cli.Nome);
            b.getComandoSQL().Parameters.AddWithValue("@nacionalidade", cli.Nacionalidade);
            b.getComandoSQL().Parameters.AddWithValue("@estadocivil", cli.Estadocivil);
            b.getComandoSQL().Parameters.AddWithValue("@RG", cli.RG);
            b.getComandoSQL().Parameters.AddWithValue("@CPF", cli.CPF);
            b.getComandoSQL().Parameters.AddWithValue("@CNPJ", cli.CNPJ);
            b.getComandoSQL().Parameters.AddWithValue("@rua", cli.Rua);
            b.getComandoSQL().Parameters.AddWithValue("@bairro", cli.Bairro);
            b.getComandoSQL().Parameters.AddWithValue("@cidade", cli.Cidade.Cod);
            b.getComandoSQL().Parameters.AddWithValue("@numero", cli.Numero);
            b.getComandoSQL().Parameters.AddWithValue("@CEP", cli.Cep);
            b.getComandoSQL().Parameters.AddWithValue("@email", cli.Email);
            b.getComandoSQL().Parameters.AddWithValue("@contato", cli.Contato);
            b.getComandoSQL().Parameters.AddWithValue("@tipocliente", cli.Tipocliente);



            b.getComandoSQL().CommandText = @"insert into clientes (nome,nacionalidade,estadocivil,RG,CPF,CNPJ,rua,bairro,cidade,numero,CEP,email,contato,tipocliente) 
                                                               values(@nome,@nacionalidade,@estadocivil,@RG,@CPF,@CNPJ,@rua,@bairro,@cidade,@numero,@CEP,@email,@contato,@tipocliente)";

            return b.ExecutaComando() == 1;
        }

        internal List<Models.Cliente> ObterTodos()
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select codigo, nome,nacionalidade,estadocivil,RG,CPF,CNPJ,rua,bairro,cidade,numero,CEP,email,contato,tipocliente
                                              from clientes";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal Models.Cliente ObterCliente(int cod)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@codigo", cod);

            b.getComandoSQL().CommandText = @"select codigo, nome,nacionalidade,estadocivil,RG,CPF,CNPJ,rua,bairro,cidade,numero,CEP,email,contato,tipocliente
                                              from clientes
                                              where codigo = @codigo";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                Models.Cidade cid = new DAL.CidadeDAL().ObterCidade((int)row["cidade"]);

                Models.Cliente cliente = new Models.Cliente((int)row["codigo"], row["nome"].ToString(), row["nacionalidade"].ToString(),
                                                                    row["estadocivil"].ToString(), row["RG"].ToString(), row["CPF"].ToString(),
                                                                    row["CNPJ"].ToString(), row["rua"].ToString(), row["bairro"].ToString(), cid,
                                                                    row["numero"].ToString(), row["CEP"].ToString(), row["email"].ToString(), row["contato"].ToString(),
                                                                      (int)row["tipocliente"]);
                
                return cliente;
              
            }

               
            else
                return null;
        }

        internal bool Excluir(int cod)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", cod);

            b.getComandoSQL().CommandText = @"delete from clientes
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }

        internal List<Models.Cliente> verificaCPFexistente(string CPFCNPJ)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@cpf", CPFCNPJ);

            b.getComandoSQL().CommandText = @"select * 
                                            from clientes
                                            where cpf = @cpf";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.Cliente> verificaCNPJexistente(string CPFCNPJ)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@cnpj", CPFCNPJ);

            b.getComandoSQL().CommandText = @"select * 
                                            from clientes
                                            where cnpj = @cnpj";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal bool Alterar(Models.Cliente cli)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", cli.Cod);
            b.getComandoSQL().Parameters.AddWithValue("@nome", cli.Nome);
            b.getComandoSQL().Parameters.AddWithValue("@nacionalidade", cli.Nacionalidade);
            b.getComandoSQL().Parameters.AddWithValue("@estadocivil", cli.Estadocivil);
            b.getComandoSQL().Parameters.AddWithValue("@RG", cli.RG);
            b.getComandoSQL().Parameters.AddWithValue("@CPF", cli.CPF);
            b.getComandoSQL().Parameters.AddWithValue("@CNPJ", cli.CNPJ);
            b.getComandoSQL().Parameters.AddWithValue("@rua", cli.Rua);
            b.getComandoSQL().Parameters.AddWithValue("@bairro", cli.Bairro);
            b.getComandoSQL().Parameters.AddWithValue("@cidade", cli.Cidade.Cod);
            b.getComandoSQL().Parameters.AddWithValue("@numero", cli.Numero);
            b.getComandoSQL().Parameters.AddWithValue("@CEP", cli.Cep);
            b.getComandoSQL().Parameters.AddWithValue("@email", cli.Email);
            b.getComandoSQL().Parameters.AddWithValue("@contato", cli.Contato);
            b.getComandoSQL().Parameters.AddWithValue("@tipocliente", cli.Tipocliente);

            b.getComandoSQL().CommandText = @"update clientes
                                              set nome = @nome,nacionalidade = @nacionalidade,estadocivil = @estadocivil,RG = @RG,CPF = @CPF,CNPJ = @CNPJ,rua = @rua,bairro = @bairro,
                                              cidade = @cidade, numero = @numero,CEP = @CEP,email = @email, contato = @contato,tipocliente = @tipocliente
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }
    }
}
