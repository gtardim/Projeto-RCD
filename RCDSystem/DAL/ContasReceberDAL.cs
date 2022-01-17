using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.DAL
{
    public class ContasReceberDAL
    {

        private Banco b;
        internal ContasReceberDAL()
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

                    Models.Cliente cliente = new Models.Cliente();
                    int cli_cod = Convert.ToInt32(row["clientecod"]);
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
                                       Convert.IsDBNull(row["pago"]) ? null : (DateTime?)Convert.ToDateTime(row["pago"]),
                                       Convert.ToBoolean(row["possuiparcela"]));



                    dados.Add(receber);
                }
                return dados;

            }
            else
                return null;
        }

        internal Boolean Gravar(Models.ContasReceber contas)
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

            if (contas.Pago == null)
            {
                b.getComandoSQL().CommandText = @"insert into contasreceber (codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,possuiparcela) 
                                                               values(@codigo,@contamae,@origem,@tipo,@descricao,@cliente,@valor,@valorpago,@datageracao,@possuiparcela)";
            }
            else
            {
                b.getComandoSQL().Parameters.AddWithValue("@pago", DateTime.Now);
                b.getComandoSQL().CommandText = @"insert into contasreceber (codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela) 
                                                               values(@codigo,@contamae,@origem,@tipo,@descricao,@cliente,@valor,@valorpago,@datageracao,@pago,@possuiparcela)";

            }

            return b.ExecutaComando() == 1;
        }

        internal List<Models.ContasReceber> ObterTodos()
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela
                                              from contasreceber";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.ContasReceber> ObterContas(int cliente,DateTime? datai , DateTime? dataf)
        {
            b.getComandoSQL().Parameters.Clear();

            if (cliente == 0)
            {
                if(datai == null || dataf == null)
                {
                    b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela
                                              from contasreceber";
                }
                else
                {

                    b.getComandoSQL().Parameters.AddWithValue("@datageracao", datai);
                    b.getComandoSQL().Parameters.AddWithValue("@datageracao", DateTime.Now);
                    b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela
                                              from contasreceber
                                              where datageracao between @datai and @dataf";
                }

            }
            else
            {
                if (datai == null || dataf == null)
                {
                    b.getComandoSQL().Parameters.AddWithValue("@cliente", cliente);

                    b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela
                                              from contasreceber
                                              where cliente = @cliente";
                }
                else
                {
                    b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela
                                              from contasreceber
                                              where cliente = @cliente and
                                              datageracao between @datai and @dataf";
                }

            }
           
            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal Models.ContasReceber ObterRecebimento(int codigo , int tipo)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@codigo",codigo);
            b.getComandoSQL().Parameters.AddWithValue("@tipo", tipo);

            b.getComandoSQL().CommandText = @"select codigo,contamae,origem,tipo,descricao,cliente,valor,valorpago,datageracao,pago,possuiparcela
                                              from contasreceber
                                              where codigo = @codigo and
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
                                           Convert.IsDBNull(row["pago"]) ? null : (DateTime?)Convert.ToDateTime(row["pago"]),
                                           Convert.ToBoolean(row["possuiparcela"]));

                return receber;

            }


            else
                return null;
        }


        internal bool Excluir(int codigo,int tipo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", codigo);
            b.getComandoSQL().Parameters.AddWithValue("@tipo", tipo);

            b.getComandoSQL().CommandText = @"delete from contasreceber
                                              where codigo = @codigo
                                                and   tipo = @tipo";

            return b.ExecutaComando() == 1;
        }

        internal bool ExcluirProt(int codigo, int tipo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", codigo);
            b.getComandoSQL().Parameters.AddWithValue("@tipo", tipo);

            b.getComandoSQL().CommandText = @"delete from contasreceber
                                              where origem = @codigo
                                                and   tipo = @tipo";

            return b.ExecutaComando() == 1;
        }

        internal Decimal ObterValorRecebido(int origem, int tipo)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@origem", origem);
            b.getComandoSQL().Parameters.AddWithValue("@tipo", tipo);


            b.getComandoSQL().CommandText = @"select SUM(valorpago) as valorrecebido
                                              from contasreceber
                                              where origem = @origem and
                                                tipo = @tipo and 
                                                 pago is not null";

            DataTable dt = b.ExecutaSelect();
            if (dt.Rows[0]["valorrecebido"] != DBNull.Value && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                decimal valorecebido = (decimal)row["valorrecebido"];
                return valorecebido;
            }   
            else
                return 0;
        }

        internal bool AtualizaRecebimento(int codigo, int tipo,decimal valortotal)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", codigo);
            b.getComandoSQL().Parameters.AddWithValue("@tipo", tipo);
            b.getComandoSQL().Parameters.AddWithValue("@valortotal", valortotal);

            b.getComandoSQL().CommandText = @"update contasreceber
                                              set valor = @valortotal
                                              where origem = @codigo and
                                                    tipo = @tipo and
                                                    contamae = -1";

            return b.ExecutaComando() == 1;
        }
    }
}
