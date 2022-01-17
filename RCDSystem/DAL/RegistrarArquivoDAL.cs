using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.DAL
{
    public class RegistrarArquivoDAL
    {
        private Banco b;
        internal RegistrarArquivoDAL()
        {
            b = Banco.GetInstance();
        }

        internal List<Models.Arquivo>  TableToList(DataTable dt)
        {
           
            List<Models.Arquivo> dados = new List<Models.Arquivo>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Models.TipoArquivo tipoarq = new Models.TipoArquivo();
                    int tipoarq_cod = Convert.ToInt32(row["tipoarquivo"]);
                    tipoarq = new DAL.TipoArquivoDAL().ObterTipo(tipoarq_cod);

                    Models.Cliente cliente = new Models.Cliente();
                    int cli_cod = Convert.ToInt32(row["clientecod"]);
                    cliente = new DAL.ClienteDAL().ObterCliente(cli_cod);
                    int processo = 0;

                    if (row["processocod"] != DBNull.Value)
                        processo = (int)row["processocod"];
                    
                        

                    Models.Arquivo arq = new Models.Arquivo((int)row["codigo"],
                                                            cliente,
                                                            processo,
                                                            tipoarq,
                                                            row["descricao"].ToString(),
                                                            (Byte[])row["arquivo"],
                                                            Convert.ToDateTime(row["DataInclusao"]),
                                                            row["nome"].ToString(),
                                                            row["formato"].ToString(),
                                                            row["tipo"].ToString(),
                                                            (int)row["tamanho"]);
                                                            


                    dados.Add(arq);
                }
                return dados;

            }
            else
                return null;
        }

        internal Boolean Gravar(Models.Arquivo arq)
        {
            b.getComandoSQL().Parameters.Clear();

            if (arq.Processocod == 0)
                b.getComandoSQL().Parameters.AddWithValue("@processocod", DBNull.Value);
            else
                b.getComandoSQL().Parameters.AddWithValue("@processocod", arq.Processocod);

            b.getComandoSQL().Parameters.AddWithValue("@clientecod", arq.Cli.Cod);
            b.getComandoSQL().Parameters.AddWithValue("@tipoarquivo", arq.Tipoarq.Cod_tipo);
            b.getComandoSQL().Parameters.AddWithValue("@descricao", arq.Descricao);
            b.getComandoSQL().Parameters.AddWithValue("@arquivo", arq.Arq);
            b.getComandoSQL().Parameters.AddWithValue("@datainclusao", arq.DataInclusao);
            b.getComandoSQL().Parameters.AddWithValue("@nome", arq.Nome);
            b.getComandoSQL().Parameters.AddWithValue("@formato", arq.Formato);
            b.getComandoSQL().Parameters.AddWithValue("@tipo", arq.Tipo);
            b.getComandoSQL().Parameters.AddWithValue("@tamanho", arq.Tamanho);
            b.getComandoSQL().Parameters.AddWithValue("@data", DateTime.Now);


            b.getComandoSQL().CommandText = @"insert into arquivos (clientecod,processocod,tipoarquivo,descricao,arquivo,nome,formato,tipo,tamanho,datainclusao) 
                                                               values(@clientecod,@processocod,@tipoarquivo,@descricao,@arquivo,@nome,@formato,@tipo,@tamanho,@data)";

            return b.ExecutaComando() == 1;
        }

        internal List<Models.Arquivo> ObterTodos()
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select codigo,clientecod,processocod,tipoarquivo,descricao,arquivo,datainclusao,nome,formato,tipo,tamanho
                                              from arquivos
                                              where processocod is null";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.Arquivo> VerificaTipoArquivo(int cod_tipo)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@tipoarquivo", cod_tipo);

            b.getComandoSQL().CommandText = @"select codigo,clientecod,processocod,tipoarquivo,descricao,arquivo,datainclusao,nome,formato,tipo,tamanho
                                              from arquivos
                                              where tipoarquivo = @tipoarquivo";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.Arquivo> VerificaClienteArquivo(int cod)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@clientecod", cod);

            b.getComandoSQL().CommandText = @"select codigo,clientecod,processocod,tipoarquivo,descricao,arquivo,datainclusao,nome,formato,tipo,tamanho
                                              from arquivos
                                              where processocod is null and
                                              clientecod = @clientecod";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.Arquivo> ObterArquivosRP(int cliente, int tipoarq,int processo)
        {
            b.getComandoSQL().Parameters.Clear();

        if(processo == 0)
        {

                if (cliente == 0)
                {
                    if (tipoarq == 0)
                    {
                        b.getComandoSQL().CommandText = @"select codigo,clientecod,processocod,tipoarquivo,descricao,arquivo,datainclusao,nome,formato,tipo,tamanho
                                              from arquivos
                                              order by clientecod";
                    }
                    else
                    {
                        b.getComandoSQL().Parameters.AddWithValue("@tipoarq", tipoarq);

                        b.getComandoSQL().CommandText = @"select codigo,clientecod,processocod,tipoarquivo,descricao,arquivo,datainclusao,nome,formato,tipo,tamanho
                                              from arquivos
                                              where tipoarquivo = @tipoarq
                                              order by clientecod";
                    }

                }
                else
                {
                    b.getComandoSQL().Parameters.AddWithValue("@cliente", cliente);

                    if (tipoarq == 0)
                    {
                        b.getComandoSQL().CommandText = @"select codigo,clientecod,processocod,tipoarquivo,descricao,arquivo,datainclusao,nome,formato,tipo,tamanho
                                              from arquivos
                                              where clientecod = @cliente
                                              order by clientecod";
                    }
                    else
                    {
                        b.getComandoSQL().Parameters.AddWithValue("@tipoarq", tipoarq);
                        b.getComandoSQL().CommandText = @"select codigo,clientecod,processocod,tipoarquivo,descricao,arquivo,datainclusao,nome,formato,tipo,tamanho
                                              from arquivos
                                              where clientecod = @cliente and
                                              tipoarquivo = @tipoarq
                                              order by clientecod";

                    }

                }

        }
        else
        {
                b.getComandoSQL().Parameters.AddWithValue("@processo", processo);

                if (cliente == 0)
                {
                    if (tipoarq == 0)
                    {
                        b.getComandoSQL().CommandText = @"select codigo,clientecod,processocod,tipoarquivo,descricao,arquivo,datainclusao,nome,formato,tipo,tamanho
                                              from arquivos
                                              where processocod = @processo
                                              order by clientecod";
                    }
                    else
                    {
                        b.getComandoSQL().Parameters.AddWithValue("@tipoarq", tipoarq);

                        b.getComandoSQL().CommandText = @"select codigo,clientecod,processocod,tipoarquivo,descricao,arquivo,datainclusao,nome,formato,tipo,tamanho
                                              from arquivos
                                              where tipoarquivo = @tipoarq and
                                              processocod = @processo
                                              order by clientecod";
                    }

                }
                else
                {
                    b.getComandoSQL().Parameters.AddWithValue("@cliente", cliente);

                    if (tipoarq == 0)
                    {
                        b.getComandoSQL().CommandText = @"select codigo,clientecod,processocod,tipoarquivo,descricao,arquivo,datainclusao,nome,formato,tipo,tamanho
                                              from arquivos
                                              where clientecod = @cliente and
                                              processocod = @processo
                                              order by clientecod";
                    }
                    else
                    {
                        b.getComandoSQL().Parameters.AddWithValue("@tipoarq", tipoarq);
                        b.getComandoSQL().CommandText = @"select codigo,clientecod,processocod,tipoarquivo,descricao,arquivo,datainclusao,nome,formato,tipo,tamanho
                                              from arquivos
                                              where clientecod = @cliente and
                                              tipoarquivo = @tipoarq and
                                              processocod = @processo
                                              order by clientecod";

                    }

                }

        }
           


            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.Arquivo> ObterArquivosProcesso(int cod)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@processocod", cod);

            b.getComandoSQL().CommandText = @"select codigo,clientecod,processocod,tipoarquivo,descricao,arquivo,datainclusao,nome,formato,tipo,tamanho
                                              from arquivos
                                               where processocod = @processocod";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal Models.Arquivo GetArquivo(int id)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@codigo",id);

            b.getComandoSQL().CommandText = @"select arquivo,tipo,formato,nome
                                              from arquivos
                                              where codigo = @codigo";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {

                DataRow row = dt.Rows[0];
                Models.Arquivo arquivo = new Models.Arquivo((Byte[])row["arquivo"], row["tipo"].ToString(), row["formato"].ToString(), row["nome"].ToString());
                return arquivo;
            }
                
            else
                return null;
        }

        internal Models.Arquivo GetArquivo2(int id)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@codigo", id);

            b.getComandoSQL().CommandText = @"select arquivo,tipo,formato,nome,tamanho
                                              from arquivos
                                              where codigo = @codigo";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {

                DataRow row = dt.Rows[0];
                Models.Arquivo arquivo = new Models.Arquivo((Byte[])row["arquivo"], row["tipo"].ToString(), row["formato"].ToString(), row["nome"].ToString(),(int)row["tamanho"]);
                return arquivo;
            }

            else
                return null;
        }

        internal Models.Arquivo GetArquivoFull(int id)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@codigo", id);

            b.getComandoSQL().CommandText = @"select arquivo,tipo,formato,nome,tamanho
                                              from arquivos
                                              where codigo = @codigo";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {

                DataRow row = dt.Rows[0];
                Models.Arquivo arquivo = new Models.Arquivo((Byte[])row["arquivo"], row["tipo"].ToString(), row["formato"].ToString(), row["nome"].ToString(), (int)row["tamanho"]);
                return arquivo;
            }

            else
                return null;
        }

        internal bool Excluir(int cod)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", cod);

            b.getComandoSQL().CommandText = @"delete from arquivos
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }

        internal bool ExcluirArqProcesso(int cod)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", cod);

            b.getComandoSQL().CommandText = @"delete from arquivos
                                              where processocod = @codigo";

            return b.ExecutaComando() == 1;
        }

        internal bool Alterar(Models.Arquivo arq)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", arq.Cod);
            b.getComandoSQL().Parameters.AddWithValue("@clientecod", arq.Cli.Cod);
            b.getComandoSQL().Parameters.AddWithValue("@processocod", arq.Processocod);
            b.getComandoSQL().Parameters.AddWithValue("@tipoarquivo", arq.Tipoarq.Cod_tipo);
            b.getComandoSQL().Parameters.AddWithValue("@descricao", arq.Descricao);
            b.getComandoSQL().Parameters.AddWithValue("@arquivo", arq.Arq);
            b.getComandoSQL().Parameters.AddWithValue("@datainclusao", arq.DataInclusao);
            b.getComandoSQL().Parameters.AddWithValue("@nome", arq.Nome);
            b.getComandoSQL().Parameters.AddWithValue("@formato", arq.Formato);
            b.getComandoSQL().Parameters.AddWithValue("@tipo", arq.Tipo);
            b.getComandoSQL().Parameters.AddWithValue("@tamanho", arq.Tamanho);

            b.getComandoSQL().CommandText = @"update arquivos
                                            set clientecod = @clientecod,
                                                processocod = @processocod,
                                                tipoarquivo = @tipoarquivo,
                                                descricao = @descricao,
                                                arquivo = @arquivo,
                                                datainclusao = @datainclusao,
                                                nome = @nome,
                                                formato = @formato,
                                                tipo = @tipo,
                                                tamanho = @tamanho                       
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }

        internal bool ValidaDescricao(string descricao,int processocod)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@descricao", descricao);
            b.getComandoSQL().Parameters.AddWithValue("@processocod", processocod);

            b.getComandoSQL().CommandText = @"select codigo,clientecod,processocod,tipoarquivo,descricao,arquivo,datainclusao,nome,formato,tipo,tamanho
                                              from arquivos
                                              where descricao = @descricao and 
                                                  processocod = @processocod";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return false;

            else
                return true;
        }

    }
}
