using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.DAL
{
    public class QuitarContasReceberDAL
    {

        private Banco b;
        internal QuitarContasReceberDAL()
        {
            b = Banco.GetInstance();
        }

        internal List<Models.ContasReceber> TableToList(DataTable dt)
        {

            List<Models.ContasReceber> dados = new List<Models.ContasReceber>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {

                    int cliente = Convert.ToInt32(row["cliente"]);
                    Models.Cliente cli = new DAL.ClienteDAL().ObterCliente(cliente);




                    Models.ContasReceber recebimento = new Models.ContasReceber((int)row["codigo"],
                                                                         (int)row["contamae"],
                                                                         (int)row["origem"],
                                                                         (int)row["tipo"],
                                                           row["descricao"].ToString(),
                                                           cli,
                                                           (decimal)row["valor"],
                                                           (decimal)row["valorpago"],
                                                           Convert.ToDateTime(row["datageracao"]),
                                                           Convert.IsDBNull(row["pago"]) ? null : (DateTime?)Convert.ToDateTime(row["pago"]),
                                                           Convert.ToBoolean(row["possuiparcela"]));



                    dados.Add(recebimento);
                }
                return dados;

            }
            else
                return null;
        }


        internal List<Models.ContasReceber> ObterAbertas()
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela
                                              from contasreceber
                                              where pago is null 
                                              order by descricao";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.ContasReceber> ObterAbertas1Data(DateTime dataini)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@dataini", dataini);

            b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela
                                              from contasreceber
                                              where pago is null and datageracao = @dataini
                                              order by descricao";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.ContasReceber> ObterAbertas2Data(DateTime dataini, DateTime datafim)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@dataini", dataini);
            b.getComandoSQL().Parameters.AddWithValue("@datafim", datafim);

            b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela
                                              from contasreceber
                                              where pago is null and datageracao between @dataini and @datafim
                                              order by descricao";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.ContasReceber> ObterPagas()
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela
                                              from contasreceber
                                              where pago is not null 
                                              order by descricao";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }


        internal List<Models.ContasReceber> ObterPagas1Data(DateTime dataini)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@dataini", dataini);

            b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela
                                              from contasreceber
                                              where pago is not null and datageracao = @dataini
                                              order by descricao";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.ContasReceber> ObterPagas2Data(DateTime dataini, DateTime datafim)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@dataini", dataini);
            b.getComandoSQL().Parameters.AddWithValue("@datafim", datafim);

            b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela
                                              from contasreceber
                                              where pago is not null  and datageracao between @dataini and @datafim 
                                              order by descricao";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal Models.ContasReceber ObterPaga(int codigo)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@codigo", codigo);

            b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela
                                              from contasreceber
                                              where pago is not null and
                                              codigo = @codigo";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                int cliente = Convert.ToInt32(row["cliente"]);
                Models.Cliente cli = new DAL.ClienteDAL().ObterCliente(cliente);

                Models.ContasReceber recebimento = new Models.ContasReceber((int)row["codigo"],
                                                                     (int)row["contamae"],
                                                                     (int)row["origem"],
                                                                     (int)row["tipo"],
                                                       row["descricao"].ToString(),
                                                       cli,
                                                       (decimal)row["valor"],
                                                       (decimal)row["valorpago"],
                                                       Convert.ToDateTime(row["datageracao"]),
                                                       Convert.ToDateTime(row["pago"]),
                                                       Convert.ToBoolean(row["possuiparcela"]));


                return recebimento;

            }


            else
                return null;
        }

        internal bool ReceberTotal(Models.ContasReceber Conta)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", Conta.Codigo);
            b.getComandoSQL().Parameters.AddWithValue("@valorpago", Conta.Valor);
            b.getComandoSQL().Parameters.AddWithValue("@tipo", Conta.Tipo);
            b.getComandoSQL().Parameters.AddWithValue("@pago", DateTime.Now);


            b.getComandoSQL().CommandText = @"update contasreceber
                                              set pago = @pago, valorpago = @valorpago
                                              where codigo = @codigo and
                                                    tipo = @tipo";

            return b.ExecutaComando() == 1;
        }

        internal bool QuitarRecebimentoAnteior(int codigo, decimal valorpago, int tipo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", codigo);
            b.getComandoSQL().Parameters.AddWithValue("@valorpago", valorpago);
            b.getComandoSQL().Parameters.AddWithValue("@tipo", tipo);
            b.getComandoSQL().Parameters.AddWithValue("@pago", DateTime.Now);


            b.getComandoSQL().CommandText = @"update contasreceber
                                              set pago = @pago , valorpago = @valorpago,possuiparcela = 1
                                              where codigo = @codigo and
                                                    tipo = @tipo";

            return b.ExecutaComando() == 1;
        }

        internal Boolean GerarNovaParcela(Models.ContasReceber contas)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", contas.Codigo);
            b.getComandoSQL().Parameters.AddWithValue("@contamae", contas.Contamae);
            b.getComandoSQL().Parameters.AddWithValue("@origem", contas.Origem);
            b.getComandoSQL().Parameters.AddWithValue("@tipo", contas.Tipo);
            b.getComandoSQL().Parameters.AddWithValue("@descricao", contas.Descricao);
            b.getComandoSQL().Parameters.AddWithValue("@cliente", contas.Cliente.Cod);
            b.getComandoSQL().Parameters.AddWithValue("@valor", contas.Valor);
            b.getComandoSQL().Parameters.AddWithValue("@valorpago", contas.Valorpago);
            b.getComandoSQL().Parameters.AddWithValue("@datageracao", DateTime.Now);
            b.getComandoSQL().Parameters.AddWithValue("@possuiparcela", contas.Possuiparcela);




            b.getComandoSQL().CommandText = @"insert into contasreceber (codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,possuiparcela) 
                                                               values(@codigo,@contamae,@origem,@tipo,@descricao,@cliente,@valor,@valorpago,@datageracao,@possuiparcela)";

            return b.ExecutaComando() == 1;
        }

        internal bool EstornarUnica(int codigo, int tipo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", codigo);
            b.getComandoSQL().Parameters.AddWithValue("@tipo", tipo);


            b.getComandoSQL().CommandText = @"update contasreceber
                                              set pago = null , valorpago = 0, possuiparcela = 0
                                              where codigo = @codigo and
                                                      tipo = @tipo";

            return b.ExecutaComando() == 1;
        }

        internal bool ApagaProximaParcela(int codigo, int tipo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", codigo);
            b.getComandoSQL().Parameters.AddWithValue("@tipo", tipo);

            b.getComandoSQL().CommandText = @"delete from contasreceber
                                              where contamae = @codigo and
                                                        tipo = @tipo";

            return b.ExecutaComando() == 1;
        }



        internal bool VerificaProxima(int codigo, int tipo)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@codigo", codigo);
            b.getComandoSQL().Parameters.AddWithValue("@tipo", tipo);

            b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela
                                              from contasreceber
                                              where contamae = @codigo and
                                                      tipo = @tipo and 
                                                      pago is null";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        internal Models.ContasReceber ObterProxima(int codigo, int tipo)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@codigo", codigo);
            b.getComandoSQL().Parameters.AddWithValue("@tipo", tipo);

            b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela
                                              from contasreceber
                                              where contamae = @codigo and
                                                    tipo = @tipo";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                Models.Cliente cliente = new Models.Cliente();
                int cli_cod = Convert.ToInt32(row["cliente"]);
                cliente = new DAL.ClienteDAL().ObterCliente(cli_cod);


                Models.ContasReceber receber = new Models.ContasReceber((int)row["codigo"],
                                                                      (int)row["contamae"],
                                                                      (int)row["origem"],
                                                                      (int)row["tipo"],
                                                        row["descricao"].ToString(),
                                                        cliente,
                                                        (decimal)row["valor"],
                                                        (decimal)row["valorpago"],
                                                        Convert.ToDateTime(row["datageracao"]),
                                                        Convert.ToDateTime(row["pago"]),
                                                        Convert.ToBoolean(row["possuiparcela"]));

                return receber;

            }
            else
                return null;
        }

        internal List<Models.ContasReceber> ObterRecebidasRProt(int cliente, DateTime? datai, DateTime? dataf)
        {
            b.getComandoSQL().Parameters.Clear();

            if (cliente == 0)
            {
                if (datai == dataf)
                {

                    if (datai != null)
                    {
                        b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                        b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);
                        b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela 
                                                      from contasreceber 
                                                      where pago is not null and 
                                                      tipo = 1 and
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                    }
                    else
                    {
                        b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela 
                                                      from contasreceber 
                                                      where pago is not null and 
                                                      tipo = 1 order by origem";

                    }


                   
                }
                else
                {
                    if (datai == null || dataf == null)
                    {
                        b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela 
                                                      from contasreceber 
                                                      where pago is not null and 
                                                      tipo = 1 order by origem";
                    }
                    else
                    {

                        b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                        b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);
                        b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela 
                                                      from contasreceber 
                                                      where pago is not null and 
                                                      tipo = 1 and
                                                      datageracao between @datai and @dataf
                                                      order by origem";
                    }

                }
            }
            else
            {
                b.getComandoSQL().Parameters.AddWithValue("@cliente", cliente);

                if(datai == dataf)
                {

                    if (datai != null)
                    {
                        b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela 
                                                      from contasreceber 
                                                      where cliente = @cliente and
                                                      pago is not null and 
                                                      tipo = 1 and
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                    }
                    else
                    {
                        b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela 
                                                      from contasreceber 
                                                      where is not null and 
                                                      cliente = @cliente and
                                                      tipo = 1 order by origem";

                    }




                    
                }
                else
                {
                    if (datai == null || dataf == null)
                    {


                        b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela 
                                                      from contasreceber 
                                                      where cliente = @cliente
                                                      pago is not null and 
                                                      tipo = 1 
                                                      order by origem";
                    }
                    else
                    {
                        b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                        b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);
                        b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela 
                                                      from contasreceber 
                                                      where cliente = @cliente
                                                      pago is not null and 
                                                      tipo = 1 and
                                                      datageracao between @datai and @dataf
                                                      order by origem";
                    }

                }
                

            }




            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }


        internal List<Models.ContasReceber> ObterRecebidasRProc(int cliente, DateTime? datai, DateTime? dataf)
        {
            b.getComandoSQL().Parameters.Clear();

            if (cliente == 0)
            {
                if(datai == dataf)
                {
                    b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                    b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);
                    b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela 
                                                      from contasreceber 
                                                      where pago is not null and 
                                                      tipo = 2 and
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";

                }
                else
                {
                    if (datai == null || dataf == null)
                    {
                        b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela 
                                                      from contasreceber 
                                                      where pago is not null and 
                                                      tipo = 2 order by origem";
                    }
                    else
                    {

                        b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                        b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);
                        b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela 
                                                      from contasreceber 
                                                      where pago is not null and 
                                                      tipo = 2 and
                                                      datageracao between @datai and @dataf
                                                      order by origem";
                    }

                }
               

            }
            else
            {
                b.getComandoSQL().Parameters.AddWithValue("@cliente", cliente);

                if(datai == dataf)
                {
                    b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                    b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);
                    b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela 
                                                      from contasreceber 
                                                      where cliente = @cliente
                                                      pago is not null and 
                                                      tipo = 2 and
                                                      DATEDIFF (DATAGERACAO,@datai)=0
                                                      order by origem";
                }
                else
                {
                    if (datai == null || dataf == null)
                    {


                        b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela 
                                                      from contasreceber 
                                                      where cliente = @cliente
                                                      pago is not null and 
                                                      tipo = 2 
                                                      order by origem";
                    }
                    else
                    {
                        b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                        b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);
                        b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela 
                                                      from contasreceber 
                                                      where cliente = @cliente
                                                      pago is not null and 
                                                      tipo = 2 and
                                                      datageracao between @datai and @dataf
                                                      order by origem";
                    }
                }
               

            }

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;

        }
    }

}
