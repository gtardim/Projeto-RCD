using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.DAL
{
    public class ProtocoloDAL
    {

        private Banco b;
        internal ProtocoloDAL()
        {
            b = Banco.GetInstance();
        }

        internal List<Models.Protocolo> TableToList(DataTable dt)
        {

            List<Models.Protocolo> dados = new List<Models.Protocolo>();

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






                    Models.Protocolo protocolo = new Models.Protocolo((int)row["codigo"],
                                                                          row["descricao"].ToString(),
                                                                          cli,
                                                                          catpp,
                                                                          statuspp,
                                                            (decimal)row["valortotal"],
                                                            (int)row["codprocesso"],
                                                            row["observacoes"].ToString());

                    dados.Add(protocolo);
                }
                return dados;

            }
            else
                return null;
        }

        internal List<Models.ProtocoloReport> TableToListRP(DataTable dt)
        {

            List<Models.ProtocoloReport> dados = new List<Models.ProtocoloReport>();

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






                    Models.ProtocoloReport protocolo = new Models.ProtocoloReport((int)row["codigo"],
                                                                          row["descricao"].ToString(),
                                                                          cli,
                                                                          catpp,
                                                                          statuspp,
                                                            (decimal)row["valortotal"],
                                                            (int)row["codprocesso"],
                                                            row["observacoes"].ToString(),
                                                            Convert.ToDateTime(row["datageracao"]));

                    dados.Add(protocolo);
                }
                return dados;

            }
            else
                return null;
        }


        internal Boolean Gravar(Models.Protocolo protocolo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", protocolo.Cod);
            b.getComandoSQL().Parameters.AddWithValue("@descricao", protocolo.Descricao);
            b.getComandoSQL().Parameters.AddWithValue("@cliente", protocolo.Cliente.Cod);
            b.getComandoSQL().Parameters.AddWithValue("@categoria", protocolo.Categoria.Cod_tipo);
            b.getComandoSQL().Parameters.AddWithValue("@status", protocolo.Status.Cod_tipo);
            b.getComandoSQL().Parameters.AddWithValue("@valortotal", protocolo.Valortotal);
            b.getComandoSQL().Parameters.AddWithValue("@processo", protocolo.Processo);
            b.getComandoSQL().Parameters.AddWithValue("@observacoes", protocolo.Observacoes);


            if(protocolo.Cod == 0)
            {
                b.getComandoSQL().CommandText = @"insert into protocolo (codigo,descricao,codcliente,categoria,status,valortotal,codprocesso,observacoes) 
                                                                               values(@codigo,@descricao,@cliente,@categoria,@status,@valortotal,@processo,@observacoes)";
            }
            else
            {
                b.getComandoSQL().CommandText = @"update protocolo
                                                   set descricao = @descricao ,codcliente = @cliente,categoria = @categoria,
                                                   status = @status,valortotal = @valortotal,codprocesso = @processo,observacoes = @observacoes
                                                    where codigo = @codigo";
            }

            

            return b.ExecutaComando() == 1;
        }

        internal Boolean GravaProcesso(int codprot, int processocod)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", codprot);
            b.getComandoSQL().Parameters.AddWithValue("@processo", processocod);



                b.getComandoSQL().CommandText = @"update protocolo
                                                   set codprocesso = @processo
                                                    where codigo = @codigo";
            
            return b.ExecutaComando() == 1;
        }


        internal List<Models.Protocolo> ObterTodos()
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select codigo,descricao,codcliente,categoria,status,valortotal,codprocesso,observacoes
                                              from protocolo";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.Protocolo> VerificaStatusPP(int cod)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@status", cod);

            b.getComandoSQL().CommandText = @"select codigo,descricao,codcliente,categoria,status,valortotal,codprocesso,observacoes
                                              from protocolo
                                              where status = @status";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.Protocolo> VerificaProtocoloCliente(int cod)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@codcliente", cod);

            b.getComandoSQL().CommandText = @"select codigo,descricao,codcliente,categoria,status,valortotal,codprocesso,observacoes
                                              from protocolo
                                              where codcliente = @codcliente";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.Protocolo> ValidaCategoriaPP(int cod_tipo)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@categoria", cod_tipo);

            b.getComandoSQL().CommandText = @"select codigo,descricao,codcliente,categoria,status,valortotal,codprocesso,observacoes
                                              from protocolo
                                              where categoria = @categoria";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.Protocolo> ObterProtocolosFiltroRP()
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select p.codigo,p.descricao,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes
                                              from protocolo p
                                              inner join atendimento a on p.codigo = a.codigoprotocolo
                                              group by p.codigo";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.ProtocoloReport> ObterProtocolosRP(int cliente,int status, DateTime? datai, DateTime? dataf)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
            b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);
            

            if (cliente == 0)
            {
                if(status == 0)
                {
                    if (datai == null || dataf == null)
                    {
                        if (datai != null)
                        {
                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes,c.datageracao 
                                                      from protocolo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR and
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                        }
                        else
                        {
                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes,c.datageracao 
                                                      from protocolo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR
                                                      order by origem";


                        }

                    }
                    else
                    {

                        if (datai != null && datai == dataf)
                        {
                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes,c.datageracao 
                                                      from protocolo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR AND
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                        }
                        else
                        {

                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes,c.datageracao 
                                                      from protocolo p 
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
                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes,c.datageracao 
                                                      from protocolo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR and
                                                      p.status = @status and
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                        }
                        else
                        {
                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes,c.datageracao 
                                                      from protocolo p 
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
                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes,c.datageracao 
                                                      from protocolo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR AND
                                                      p.status = @status and  
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                        }
                        else
                        {

                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes,c.datageracao 
                                                      from protocolo p 
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

                if(status == 0)
                {
                    if (datai == null || dataf == null)
                    {
                        if (datai != null)
                        {
                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes,c.datageracao 
                                                      from protocolo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR and
                                                      p.codcliente = @cliente AND
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                        }
                        else
                        {
                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes,c.datageracao 
                                                      from protocolo p 
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
                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes,c.datageracao 
                                                      from protocolo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR AND
                                                      p.codcliente = @cliente AND
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                        }
                        else
                        {

                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes,c.datageracao 
                                                      from protocolo p 
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
                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes,c.datageracao 
                                                      from protocolo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR and
                                                      p.codcliente = @cliente AND
                                                      p.status = @status and
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                        }
                        else
                        {
                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes,c.datageracao 
                                                      from protocolo p 
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
                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes,c.datageracao 
                                                      from protocolo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR AND
                                                      p.codcliente = @cliente AND
                                                      p.status = @status and
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                        }
                        else
                        {

                            b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes,c.datageracao 
                                                      from protocolo p 
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

        internal List<Models.ProtocoloReport> ObterRecebimentos(int cliente, DateTime? datai, DateTime? dataf)
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
                        b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes,c.datageracao 
                                                      from protocolo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR and
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                    }
                    else
                    {
                        b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes,c.datageracao 
                                                      from protocolo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR
                                                      order by origem";


                    }

                }
                else
                {

                    if (datai != null && datai == dataf)
                    {
                        b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes,c.datageracao 
                                                      from protocolo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR AND
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                    }
                    else
                    {
                        
                        b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes,c.datageracao 
                                                      from protocolo p 
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
                        b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes,c.datageracao 
                                                      from protocolo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR and
                                                      p.codcliente = @cliente AND
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                    }
                    else
                    {
                        b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes,c.datageracao 
                                                      from protocolo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR
                                                      p.codcliente = @cliente AND
                                                      order by origem";


                    }

                }
                else
                {
                    if (datai != null && datai == dataf)
                    {
                        b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes,c.datageracao 
                                                      from protocolo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR AND
                                                      p.codcliente = @cliente AND
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                    }
                    else
                    {

                        b.getComandoSQL().CommandText = @"select p.CODIGO,p.DESCRICAO,p.codcliente,p.categoria,p.status,p.valortotal,p.codprocesso,p.observacoes,c.datageracao 
                                                      from protocolo p 
                                                      inner join contasreceber c on p.CODIGO = c.ORIGEM AND 
                                                      p.VALORTOTAL = c.VALOR AND
                                                      p.codcliente = @cliente AND
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



        internal Models.Protocolo ObterProtocolo(int cod)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@codigo",cod);

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

            b.getComandoSQL().CommandText = @"select codigo,descricao,codcliente,categoria,status,valortotal,codprocesso,observacoes
                                              from protocolo
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






                Models.Protocolo protocolo = new Models.Protocolo((int)row["codigo"],
                                                                      row["descricao"].ToString(),
                                                                      cli,
                                                                      catpp,
                                                                      statuspp,
                                                        (decimal)row["valortotal"],
                                                        (int)row["codprocesso"],
                                                        row["observacoes"].ToString());

                return protocolo.Cod;

            }


            else
                return 0;
        }

        internal bool ValidaDescricao(string descricao)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@descricao", descricao);

            b.getComandoSQL().CommandText = @"select codigo,descricao,codcliente,categoria,status,valortotal,codprocesso,observacoes
                                              from protocolo
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

            b.getComandoSQL().CommandText = @"delete from protocolo
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }
       
    }

}
