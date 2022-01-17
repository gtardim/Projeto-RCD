using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.DAL
{
    public class QuitarContasPagarDAL
    {

        private Banco b;
        internal QuitarContasPagarDAL()
        {
            b = Banco.GetInstance();
        }

        internal List<Models.ContasPagar> TableToList(DataTable dt)
        {

            List<Models.ContasPagar> dados = new List<Models.ContasPagar>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    
                    int tipodespesa_cod = Convert.ToInt32(row["tipodespesa"]);
                    Models.TipoDespesa tipodespesa = new DAL.TipoDespesaDAL().ObterDespesa(tipodespesa_cod);




                    Models.ContasPagar despesa = new Models.ContasPagar((int)row["codigo"],
                                                                          (int)row["contamae"],
                                                            Convert.ToDateTime(row["DataVencimento"]),
                                                            row["descricao"].ToString(),
                                                            row["detalhes"].ToString(),
                                                            Convert.ToBoolean(row["pago"]),
                                                            Convert.ToBoolean(row["possuiparcela"]),
                                                            tipodespesa,
                                                            (decimal)row["valor"],
                                                            (decimal)row["valorpago"]);



                    dados.Add(despesa);
                }
                return dados;

            }
            else
                return null;
        }


        internal Models.ContasPagar ObterDespesa(int codigo)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@codigo", codigo);

            b.getComandoSQL().CommandText = @"select codigo,contamae,datavencimento,descricao,detalhes,pago,possuiparcela,tipodespesa,valor,valorpago
                                              from contaspagar
                                              where codigo = @codigo";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                int tipodespesa_cod = Convert.ToInt32(row["tipodespesa"]);
                Models.TipoDespesa tipodespesa = new DAL.TipoDespesaDAL().ObterDespesa(tipodespesa_cod);

                Models.ContasPagar conta = new Models.ContasPagar((int)row["codigo"],
                                                                          (int)row["contamae"],
                                                            Convert.ToDateTime(row["DataVencimento"]),
                                                            row["descricao"].ToString(),
                                                            row["detalhes"].ToString(),
                                                            Convert.ToBoolean(row["pago"]),
                                                            Convert.ToBoolean(row["possuiparcela"]),
                                                            tipodespesa,
                                                            (decimal)row["valor"],
                                                            (decimal)row["valorpago"]);

                return conta;

            }


            else
                return null;
        }

        internal List<Models.ContasPagar> ObterAbertas()
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select codigo,contamae,datavencimento,descricao,detalhes,pago,possuiparcela,tipodespesa,valor,valorpago
                                              from contaspagar
                                              where pago = 0";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.ContasPagar> ObterVencidasRP()
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@dataatual", DateTime.Now);

            b.getComandoSQL().CommandText = @"select codigo,contamae,datavencimento,descricao,detalhes,pago,possuiparcela,tipodespesa,valor,valorpago
                                              from contaspagar
                                              where pago = 0 and
                                              datavencimento < @dataatual
                                              order by datavencimento";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.ContasPagar> ObterAbertasRP(int tipodes, DateTime? datai, DateTime? dataf)
        {

            b.getComandoSQL().Parameters.Clear();

            if (tipodes == 0)
            {
                if (datai == dataf)
                {
                    if(datai != null)
                    {
                        b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                        b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);
                        b.getComandoSQL().CommandText = @"select codigo, contamae, datavencimento, descricao, detalhes, pago, possuiparcela, tipodespesa, valor, valorpago
                                                    from contaspagar
                                                    where pago = 0 and
                                                    DATEDIFF (datavencimento,@datai)=0
                                                    order by datavencimento";
                    }
                    else
                    {
                        b.getComandoSQL().CommandText = @"select codigo,contamae,datavencimento,descricao,detalhes,pago,possuiparcela,tipodespesa,valor,valorpago
                                              from contaspagar
                                              where pago = 0";

                    }
                    

                }
                else
                {
                    if (datai == null || dataf == null)
                    {
                        b.getComandoSQL().CommandText = @"select codigo,contamae,datavencimento,descricao,detalhes,pago,possuiparcela,tipodespesa,valor,valorpago
                                              from contaspagar
                                              where pago = 0";
                    }
                    else
                    {

                        b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                        b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);
                        b.getComandoSQL().CommandText = @"select codigo,contamae,datavencimento,descricao,detalhes,pago,possuiparcela,tipodespesa,valor,valorpago
                                              from contaspagar
                                              where pago = 0 and
                                              datavencimento between @datai and @dataf
                                              order by datavencimento";
                    }

                }


            }
            else
            {
                b.getComandoSQL().Parameters.AddWithValue("@tipodes", tipodes);

                if (datai == dataf)
                {

                    if (datai != null)
                    {
                        b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                        b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);
                        b.getComandoSQL().CommandText = @"select codigo, contamae, datavencimento, descricao, detalhes, pago, possuiparcela, tipodespesa, valor, valorpago
                                                    from contaspagar
                                                    where pago = 0 and
                                                    tipodespesa = @tipodes and
                                                    DATEDIFF (datavencimento,@datai)=0
                                                    order by datavencimento";
                    }
                    else
                    {
                        b.getComandoSQL().CommandText = @"select codigo,contamae,datavencimento,descricao,detalhes,pago,possuiparcela,tipodespesa,valor,valorpago
                                              from contaspagar
                                              where pago = 0 and
                                              tipodespesa = @tipodes";

                    }

                    
                }
                else
                {
                    if (datai == null || dataf == null)
                    {


                        b.getComandoSQL().CommandText = @"select codigo, contamae, datavencimento, descricao, detalhes, pago, possuiparcela, tipodespesa, valor, valorpago
                                                    from contaspagar
                                                    where pago = 0 and
                                                    tipodespesa = @tipodes
                                                    order by datavencimento";
                    }
                    else
                    {
                        b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                        b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);
                        b.getComandoSQL().CommandText = @"select codigo, contamae, datavencimento, descricao, detalhes, pago, possuiparcela, tipodespesa, valor, valorpago
                                                    from contaspagar
                                                    where pago = 0 and
                                                    tipodespesa = @tipodes and
                                                    DATEDIFF (datavencimento,@datai)=0 and
                                                    datageracao between @datai and @dataf
                                                    order by datavencimento";
                    }
                }


            }

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.ContasPagar> ObterPagasRP(int tipodes, DateTime? datai, DateTime? dataf)
        {

            b.getComandoSQL().Parameters.Clear();

            if (tipodes == 0)
            {
                if (datai == dataf)
                {
                    if (datai != null)
                    {
                        b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                        b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);
                        b.getComandoSQL().CommandText = @"select codigo, contamae, datavencimento, descricao, detalhes, pago, possuiparcela, tipodespesa, valor, valorpago
                                                    from contaspagar
                                                    where pago = 1 and
                                                    DATEDIFF (datavencimento,@datai)=0
                                                    order by datavencimento";
                    }
                    else
                    {
                        b.getComandoSQL().CommandText = @"select codigo,contamae,datavencimento,descricao,detalhes,pago,possuiparcela,tipodespesa,valor,valorpago
                                              from contaspagar
                                              where pago = 1";

                    }


                }
                else
                {
                    if (datai == null || dataf == null)
                    {
                        b.getComandoSQL().CommandText = @"select codigo,contamae,datavencimento,descricao,detalhes,pago,possuiparcela,tipodespesa,valor,valorpago
                                              from contaspagar
                                              where pago = 1";
                    }
                    else
                    {

                        b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                        b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);
                        b.getComandoSQL().CommandText = @"select codigo,contamae,datavencimento,descricao,detalhes,pago,possuiparcela,tipodespesa,valor,valorpago
                                              from contaspagar
                                              where pago = 1 and
                                              datavencimento between @datai and @dataf
                                              order by datavencimento";
                    }

                }


            }
            else
            {
                b.getComandoSQL().Parameters.AddWithValue("@tipodes", tipodes);

                if (datai == dataf)
                {

                    if (datai != null)
                    {
                        b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                        b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);
                        b.getComandoSQL().CommandText = @"select codigo, contamae, datavencimento, descricao, detalhes, pago, possuiparcela, tipodespesa, valor, valorpago
                                                    from contaspagar
                                                    where pago = 1 and
                                                    tipodespesa = @tipodes and
                                                    DATEDIFF (datavencimento,@datai)=0
                                                    order by datavencimento";
                    }
                    else
                    {
                        b.getComandoSQL().CommandText = @"select codigo,contamae,datavencimento,descricao,detalhes,pago,possuiparcela,tipodespesa,valor,valorpago
                                              from contaspagar
                                              where pago = 1 and
                                              tipodespesa = @tipodes";

                    }


                }
                else
                {
                    if (datai == null || dataf == null)
                    {


                        b.getComandoSQL().CommandText = @"select codigo, contamae, datavencimento, descricao, detalhes, pago, possuiparcela, tipodespesa, valor, valorpago
                                                    from contaspagar
                                                    where pago = 1 and
                                                    tipodespesa = @tipodes
                                                    order by datavencimento";
                    }
                    else
                    {
                        b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                        b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);
                        b.getComandoSQL().CommandText = @"select codigo, contamae, datavencimento, descricao, detalhes, pago, possuiparcela, tipodespesa, valor, valorpago
                                                    from contaspagar
                                                    where pago = 1 and
                                                    tipodespesa = @tipodes and
                                                    DATEDIFF (datavencimento,@datai)=0 and
                                                    datageracao between @datai and @dataf
                                                    order by datavencimento";
                    }
                }


            }

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.ContasPagar> ObterAbertas1Data(DateTime dataini)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@dataini", dataini);

            b.getComandoSQL().CommandText = @"select codigo,contamae,datavencimento,descricao,detalhes,pago,possuiparcela,tipodespesa,valor,valorpago
                                              from contaspagar
                                              where pago = 0 and datavencimento = @dataini";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.ContasPagar> ObterAbertas2Data(DateTime dataini, DateTime datafim)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@dataini", dataini);
            b.getComandoSQL().Parameters.AddWithValue("@datafim", datafim);

            b.getComandoSQL().CommandText = @"select codigo,contamae,datavencimento,descricao,detalhes,pago,possuiparcela,tipodespesa,valor,valorpago
                                              from contaspagar
                                              where pago = 0 and datavencimento between @dataini and @datafim";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.ContasPagar> ObterPagas()
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select codigo,contamae,datavencimento,descricao,detalhes,pago,tipodespesa,valor,valorpago,possuiparcela
                                              from contaspagar
                                              where pago = 1 ";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.ContasPagar> ObterPagas1Data(DateTime dataini)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@dataini", dataini);

            b.getComandoSQL().CommandText = @"select codigo,contamae,datavencimento,descricao,detalhes,pago,tipodespesa,valor,valorpago,possuiparcela
                                              from contaspagar
                                              where pago = 1 and datavencimento = @dataini";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.ContasPagar> ObterPagas2Data(DateTime dataini, DateTime datafim)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@dataini", dataini);
            b.getComandoSQL().Parameters.AddWithValue("@datafim", datafim);

            b.getComandoSQL().CommandText = @"select codigo,contamae,datavencimento,descricao,detalhes,pago,tipodespesa,valor,valorpago,possuiparcela
                                              from contaspagar
                                              where pago = 1  and datavencimento between @dataini and @datafim ";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal bool PagarTotal(Models.ContasPagar Conta)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", Conta.Codigo);
            b.getComandoSQL().Parameters.AddWithValue("@valorpago", Conta.Valor);


            b.getComandoSQL().CommandText = @"update contaspagar
                                              set pago = 1 , valorpago = @valorpago
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }

        internal bool QuitarParcelaAnteior(int codigo,decimal valorpago)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", codigo);
            b.getComandoSQL().Parameters.AddWithValue("@valorpago", valorpago);


            b.getComandoSQL().CommandText = @"update contaspagar
                                              set pago = 1 , valorpago = @valorpago,possuiparcela = 1
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }

        internal Boolean GerarNovaParcela(Models.ContasPagar conta)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@contamae", conta.Contamae);
            b.getComandoSQL().Parameters.AddWithValue("@datavencimento", conta.Datavenc);
            b.getComandoSQL().Parameters.AddWithValue("@descricao", conta.Descricao);
            b.getComandoSQL().Parameters.AddWithValue("@detalhe", conta.Detalhes);
            b.getComandoSQL().Parameters.AddWithValue("@pago", conta.Pago);
            b.getComandoSQL().Parameters.AddWithValue("@tipodespesa", conta.Tipodespesa.Cod_tipo);
            b.getComandoSQL().Parameters.AddWithValue("@valor", conta.Valor);
            b.getComandoSQL().Parameters.AddWithValue("@valorpago", conta.Valorpago);
            b.getComandoSQL().Parameters.AddWithValue("@possuiparcela", conta.Possuiparcela);




            b.getComandoSQL().CommandText = @"insert into contaspagar (contamae,datavencimento,descricao,detalhes,pago,tipodespesa,valor,valorpago,possuiparcela) 
                                                                values(@contamae,@datavencimento,@descricao,@detalhe,@pago,@tipodespesa,@valor,@valorpago,@possuiparcela)";

            return b.ExecutaComando() == 1;
        }

        internal bool EstornarUnica(int codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", codigo);


            b.getComandoSQL().CommandText = @"update contaspagar
                                              set pago = 0 , valorpago = 0
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }

        internal bool ApagaProximaParcela(int codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", codigo);

            b.getComandoSQL().CommandText = @"delete from contaspagar
                                              where contamae = @codigo";

            return b.ExecutaComando() == 1;
        }

        internal bool ExcluirParcela(int cod)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", cod);

            b.getComandoSQL().CommandText = @"delete from contaspagar
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }

        internal Models.ContasPagar ObterProxima(int codigo)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@codigo", codigo);

            b.getComandoSQL().CommandText = @"select codigo,contamae,datavencimento,descricao,detalhes,pago,tipodespesa,valor,valorpago,possuiparcela
                                              from contaspagar
                                              where contamae = @codigo";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                int tipodespesa_cod = Convert.ToInt32(row["tipodespesa"]);
                Models.TipoDespesa tipodespesa = new DAL.TipoDespesaDAL().ObterDespesa(tipodespesa_cod);

                Models.ContasPagar conta = new Models.ContasPagar((int)row["codigo"],
                                                                          (int)row["contamae"],
                                                            Convert.ToDateTime(row["DataVencimento"]),
                                                            row["descricao"].ToString(),
                                                            row["detalhes"].ToString(),
                                                            Convert.ToBoolean(row["pago"]),
                                                            Convert.ToBoolean(row["possuiparcela"]),
                                                            tipodespesa,
                                                            (decimal)row["valor"],
                                                            (decimal)row["valorpago"]);

                return conta;

            }


            else
                return null;
        }
    }
}
