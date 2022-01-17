using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.DAL
{
    public class ProcessoDAL
    {

        private Banco b;
        internal ProcessoDAL()
        {
            b = Banco.GetInstance();
        }

        internal List<Models.Processo> TableToList(DataTable dt)
        {

            List<Models.Processo> dados = new List<Models.Processo>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {

                    int categoria = Convert.ToInt32(row["categoria"]);
                    Models.CategoriaPP catpp = new DAL.CategoriaPPDAL().ObterCategoria(categoria);

                    int cliente = Convert.ToInt32(row["codcliente"]);
                    Models.Cliente cli = new DAL.ClienteDAL().ObterCliente(cliente);

                    int status = Convert.ToInt32(row["status"]);
                    Models.StatusPP statuspp = new DAL.StatusPPDAL().ObterStatus(status);






                    Models.Processo processo = new Models.Processo((int)row["codigo"],
                                                                          row["descricao"].ToString(),
                                                                          cli,
                                                                          catpp,
                                                                          statuspp,
                                                                          row["numeroregistro"].ToString(),
                                                            (decimal)row["valortotal"],
                                                            row["observacoes"].ToString());

                    dados.Add(processo);
                }
                return dados;

            }
            else
                return null;
        }

        internal List<Models.ProcessoReport> TableToListRP(DataTable dt)
        {

            List<Models.ProcessoReport> dados = new List<Models.ProcessoReport>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {

                    int categoria = Convert.ToInt32(row["categoria"]);
                    Models.CategoriaPP catpp = new DAL.CategoriaPPDAL().ObterCategoria(categoria);

                    int cliente = Convert.ToInt32(row["codcliente"]);
                    Models.Cliente cli = new DAL.ClienteDAL().ObterCliente(cliente);

                    int status = Convert.ToInt32(row["status"]);
                    Models.StatusPP statuspp = new DAL.StatusPPDAL().ObterStatus(status);






                    Models.ProcessoReport processo = new Models.ProcessoReport((int)row["codigo"],
                                                                          row["descricao"].ToString(),
                                                                          cli,
                                                                          catpp,
                                                                          statuspp,
                                                                          row["numeroregistro"].ToString(),
                                                            (decimal)row["valortotal"],
                                                            row["observacoes"].ToString(),
                                                            Convert.ToDateTime(row["datageracao"]));

                    dados.Add(processo);
                }
                return dados;

            }
            else
                return null;
        }


        internal Boolean Gravar(Models.Processo processo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", processo.Cod);
            b.getComandoSQL().Parameters.AddWithValue("@descricao", processo.Descricao);
            b.getComandoSQL().Parameters.AddWithValue("@cliente", processo.Cliente.Cod);
            b.getComandoSQL().Parameters.AddWithValue("@categoria", processo.Categoria.Cod_tipo);
            b.getComandoSQL().Parameters.AddWithValue("@status", processo.Status.Cod_tipo);
            b.getComandoSQL().Parameters.AddWithValue("@valortotal", processo.Valortotal);
            b.getComandoSQL().Parameters.AddWithValue("@numeroregistro", processo.Numregistro);
            b.getComandoSQL().Parameters.AddWithValue("@observacoes", processo.Observacoes);


            if (processo.Cod == 0)
            {
                b.getComandoSQL().CommandText = @"insert into processo (codigo,descricao,codcliente,categoria,status,valortotal,numeroregistro,observacoes) 
                                                                               values(@codigo,@descricao,@cliente,@categoria,@status,@valortotal,@numeroregistro,@observacoes)";
            }
            else
            {
                b.getComandoSQL().CommandText = @"update processo
                                                   set descricao = @descricao ,codcliente = @cliente,categoria = @categoria,
                                                   status = @status,valortotal = @valortotal,numeroregistro = @numeroregistro,observacoes = @observacoes
                                                    where codigo = @codigo";
            }



            return b.ExecutaComando() == 1;
        }


        internal List<Models.Processo> ObterTodos()
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select codigo,descricao,codcliente,categoria,status,valortotal,numeroregistro,observacoes
                                              from processo";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.Processo> VerificaStatusPP(int cod)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@status", cod);

            b.getComandoSQL().CommandText = @"select codigo,descricao,codcliente,categoria,status,valortotal,numeroregistro,observacoes
                                              from processo
                                              where status = @status";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.Processo> VerificaProcessoCliente(int cod)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@codcliente", cod);

            b.getComandoSQL().CommandText = @"select codigo,descricao,codcliente,categoria,status,valortotal,numeroregistro,observacoes
                                              from processo
                                              where codcliente = @codcliente";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.Processo> ValidaCategoriaPP(int cod_tipo)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@categoria", cod_tipo);

            b.getComandoSQL().CommandText = @"select codigo,descricao,codcliente,categoria,status,valortotal,numeroregistro,observacoes
                                              from processo
                                              where categoria = @categoria";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.Processo> ObterProcessoComArq()
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select p.codigo,p.descricao,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes
                                              from processo p
                                              inner join arquivos a on p.codigo = a.processocod
                                              GROUP by p.CODIGO";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }



        internal Models.Protocolo ObterProtocolo(int cod)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@codigo", cod);

            b.getComandoSQL().CommandText = @"select codigo,descricao,codcliente,categoria,status,valortotal,codprocesso,observacoes
                                              from protocolo
                                              where codigo = @codigo";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                int categoria = Convert.ToInt32(row["categoria"]);
                Models.CategoriaPP catpp = new DAL.CategoriaPPDAL().ObterCategoria(categoria);

                int cliente = Convert.ToInt32(row["codcliente"]);
                Models.Cliente cli = new DAL.ClienteDAL().ObterCliente(cliente);

                int status = Convert.ToInt32(row["status"]);
                Models.StatusPP statuspp = new DAL.StatusPPDAL().ObterStatus(status);






                Models.Protocolo protocolo = new Models.Protocolo((int)row["codigo"],
                                                                      row["descricao"].ToString(),
                                                                      cli,
                                                                      catpp,
                                                                      statuspp,
                                                        (decimal)row["valortotal"],
                                                        (int)row["codprocesso"],
                                                        row["observacoes"].ToString());

                return protocolo;

            }


            else
                return null;
        }

        internal int ObterCodNovo(string descricao)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@descricao", descricao);

            b.getComandoSQL().CommandText = @"select codigo,descricao,codcliente,categoria,status,valortotal,numeroregistro,observacoes
                                              from processo
                                              where descricao = @descricao";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                int categoria = Convert.ToInt32(row["categoria"]);
                Models.CategoriaPP catpp = new DAL.CategoriaPPDAL().ObterCategoria(categoria);

                int cliente = Convert.ToInt32(row["codcliente"]);
                Models.Cliente cli = new DAL.ClienteDAL().ObterCliente(cliente);

                int status = Convert.ToInt32(row["status"]);
                Models.StatusPP statuspp = new DAL.StatusPPDAL().ObterStatus(status);






                Models.Processo processo = new Models.Processo((int)row["codigo"],
                                                                          row["descricao"].ToString(),
                                                                          cli,
                                                                          catpp,
                                                                          statuspp,
                                                                          row["numeroregistro"].ToString(),
                                                            (decimal)row["valortotal"],
                                                            row["observacoes"].ToString());

                return processo.Cod;

            }


            else
                return 0;
        }

        internal bool ValidaDescricao(string descricao)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@descricao", descricao);

            b.getComandoSQL().CommandText = @"select codigo,descricao,codcliente,categoria,status,valortotal,numeroregistro,observacoes
                                              from processo
                                              where descricao = @descricao";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return false;

            else
                return true;
        }

        internal bool Excluir(int cod)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", cod);

            b.getComandoSQL().CommandText = @"delete from processo
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }


        internal List<Models.ProcessoReport> ObterRecebimentos(int cliente, DateTime? datai, DateTime? dataf)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
            b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);

            if (cliente == 0)
            {
                if (datai == null || dataf == null)
                {
                    if (datai != null)
                    {
                        b.getComandoSQL().CommandText = @"select p.codigo,p.descricao,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR and
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                    }
                    else
                    {
                        b.getComandoSQL().CommandText = @"select p.codigo,p.descricao,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR
                                                      order by origem";


                    }




                }
                else
                {
                    if(datai != null && datai == dataf)
                    {
                        b.getComandoSQL().CommandText = @"select p.codigo,p.descricao,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR and
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                    }
                    else
                    {

                        b.getComandoSQL().CommandText = @"select p.codigo,p.descricao,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR AND
                                                      datageracao between @datai and @dataf
                                                      order by origem";

                    }

                }

            }
            else
            {
                b.getComandoSQL().Parameters.AddWithValue("@cliente", cliente);

                if (datai == null || dataf == null)
                {

                    if (datai != null)
                    {
                        b.getComandoSQL().CommandText = @"select p.codigo,p.descricao,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR and
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                    }
                    else
                    {
                        b.getComandoSQL().CommandText = @"select p.codigo,p.descricao,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR
                                                      order by origem";

                    }


                    b.getComandoSQL().CommandText = @"select p.codigo,p.descricao,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR AND
                                                      p.codcliente = @cliente
                                                      order by origem";

                }
                else
                {
                    if (datai != null && datai == dataf)
                    {
                        b.getComandoSQL().CommandText = @"select p.codigo,p.descricao,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR and
                                                      cliente = @cliente and
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                    }
                    else
                    {
                        b.getComandoSQL().CommandText = @"select p.codigo,p.descricao,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR AND
                                                      cliente = @cliente and
                                                      datageracao between @datai and @dataf
                                                      order by origem";
                    }

                }

            }

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToListRP(dt);
            else
                return null;
        }

        internal List<Models.ProcessoReport> ObterProcessosRP(int cliente, int status, DateTime? datai, DateTime? dataf)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
            b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);


            if (cliente == 0)
            {
                if (status == 0)
                {
                    if (datai == null || dataf == null)
                    {
                        if (datai != null)
                        {
                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR and
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                        }
                        else
                        {
                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR
                                                      order by origem";


                        }

                    }
                    else
                    {

                        if (datai != null && datai == dataf)
                        {
                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR AND
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                        }
                        else
                        {

                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR AND
                                                      datageracao between @datai and @dataf
                                                      order by origem";

                        }


                    }

                }
                else
                {
                    b.getComandoSQL().Parameters.AddWithValue("@status", status);

                    if (datai == null || dataf == null)
                    {
                        if (datai != null)
                        {
                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR and
                                                      p.status = @status and
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                        }
                        else
                        {
                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.status = @status and
                                                      p.VALORTOTAL = c.VALOR
                                                      order by origem";


                        }

                    }
                    else
                    {

                        if (datai != null && datai == dataf)
                        {
                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR AND
                                                      p.status = @status and  
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                        }
                        else
                        {

                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR AND
                                                      p.status = @status and
                                                      datageracao between @datai and @dataf
                                                      order by origem";

                        }


                    }

                }




            }
            else
            {
                b.getComandoSQL().Parameters.AddWithValue("@cliente", cliente);

                if (status == 0)
                {
                    if (datai == null || dataf == null)
                    {
                        if (datai != null)
                        {
                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR and
                                                      p.codcliente = @cliente AND
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                        }
                        else
                        {
                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR and
                                                      p.codcliente = @cliente
                                                      order by origem";


                        }

                    }
                    else
                    {
                        if (datai != null && datai == dataf)
                        {
                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR AND
                                                      p.codcliente = @cliente AND
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                        }
                        else
                        {

                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR AND
                                                      p.codcliente = @cliente AND
                                                      datageracao between @datai and @dataf
                                                      order by origem";

                        }
                    }

                }
                else
                {
                    if (datai == null || dataf == null)
                    {
                        if (datai != null)
                        {
                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR and
                                                      p.codcliente = @cliente AND
                                                      p.status = @status and
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                        }
                        else
                        {
                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR and
                                                      p.codcliente = @cliente AND
                                                      p.status = @status
                                                      order by origem";


                        }

                    }
                    else
                    {
                        if (datai != null && datai == dataf)
                        {
                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR AND
                                                      p.codcliente = @cliente AND
                                                      p.status = @status and
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                        }
                        else
                        {

                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.numeroregistro,p.observacoes,c.datageracao 
                                                      from processo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR AND
                                                      p.codcliente = @cliente AND
                                                      p.status = @status and
                                                      datageracao between @datai and @dataf
                                                      order by origem";

                        }
                    }

                }



            }

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToListRP(dt);
            else
                return null;
        }

    }

}
