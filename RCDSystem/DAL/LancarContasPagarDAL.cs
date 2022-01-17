using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.DAL
{
    public class LancarContasPagarDAL
    {
        private Banco b;
        internal LancarContasPagarDAL()
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

        internal Boolean Gravar(Models.ContasPagar contas)
        {
            b.getComandoSQL().Parameters.Clear();


            b.getComandoSQL().Parameters.AddWithValue("@codigo", contas.Codigo);
            b.getComandoSQL().Parameters.AddWithValue("@datavencimento", contas.Datavenc);
            b.getComandoSQL().Parameters.AddWithValue("@descricao", contas.Descricao);
            b.getComandoSQL().Parameters.AddWithValue("@detalhes", contas.Detalhes);
            b.getComandoSQL().Parameters.AddWithValue("@pago", contas.Pago);
            b.getComandoSQL().Parameters.AddWithValue("@tipodespesa", contas.Tipodespesa.Cod_tipo);
            b.getComandoSQL().Parameters.AddWithValue("@valor", contas.Valor);
            b.getComandoSQL().Parameters.AddWithValue("@contamae", contas.Contamae);
            b.getComandoSQL().Parameters.AddWithValue("@possuiparcela",contas.Possuiparcela);
            b.getComandoSQL().Parameters.AddWithValue("@valorpago", contas.Valorpago);



            b.getComandoSQL().CommandText = @"insert into contaspagar (codigo,contamae,datavencimento,descricao,detalhes,pago,possuiparcela,tipodespesa,valor,valorpago) 
                                                               values(@codigo,@contamae,@datavencimento,@descricao,@detalhes,@pago,@possuiparcela,@tipodespesa,@valor,@valorpago)";

            return b.ExecutaComando() == 1;
        }

        internal List<Models.ContasPagar> ObterTodos()
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select codigo,contamae,datavencimento,descricao,detalhes,pago,possuiparcela,tipodespesa,valor,valorpago
                                              from contaspagar";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.ContasPagar> VerificaTipoDespesa(int cod_tipo)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@tipodespesa", cod_tipo);

            b.getComandoSQL().CommandText = @"select codigo,contamae,datavencimento,descricao,detalhes,pago,possuiparcela,tipodespesa,valor,valorpago
                                              from contaspagar
                                              where tipodespesa = @tipodespesa";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal bool Excluir(int cod)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", cod);

            b.getComandoSQL().CommandText = @"delete from contaspagar
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }

        internal bool Alterar(Models.ContasPagar contas)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", contas.Codigo);
            b.getComandoSQL().Parameters.AddWithValue("@datavencimento", contas.Datavenc);
            b.getComandoSQL().Parameters.AddWithValue("@descricao", contas.Descricao);
            b.getComandoSQL().Parameters.AddWithValue("@detalhes", contas.Detalhes);
            b.getComandoSQL().Parameters.AddWithValue("@pago", contas.Pago);
            b.getComandoSQL().Parameters.AddWithValue("@tipodespesa", contas.Tipodespesa.Cod_tipo);
            b.getComandoSQL().Parameters.AddWithValue("@valor", contas.Valor);

            b.getComandoSQL().CommandText = @"update contaspagar
                                            set 
                                                datavencimento = @datavencimento,
                                                descricao = @descricao,
                                                detalhes = @detalhes,
                                                pago = @pago,
                                                tipodespesa = @tipodespesa,
                                                valor = @valor
                                                                      
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }
    }
}
