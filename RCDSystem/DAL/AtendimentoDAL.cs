using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.DAL
{
    public class AtendimentoDAL
    {

        private Banco b;
        internal AtendimentoDAL()
        {
            b = Banco.GetInstance();
        }

        internal List<Models.Atendimento> TableToList(DataTable dt)
        {

            List<Models.Atendimento> dados = new List<Models.Atendimento>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {

                    int prot = Convert.ToInt32(row["codigoprotocolo"]);
                    Models.Protocolo protocolo = new DAL.ProtocoloDAL().ObterProtocolo(prot);

                    string teste = (row["horainicial"]).ToString();
                    TimeSpan timespan = TimeSpan.Parse(teste);
                    DateTime dataini = DateTime.Today.Add(timespan);

                    string teste2 = (row["horafinal"]).ToString();
                    TimeSpan timespan2 = TimeSpan.Parse(teste2);
                    DateTime datafim = DateTime.Today.Add(timespan2);

                    Models.Atendimento atendimento = new Models.Atendimento((int)row["codigo"],
                                                                          row["titulo"].ToString(),
                                                                          protocolo,
                                                                          Convert.ToDateTime(row["data"]),
                                                                          dataini,
                                                                          datafim,
                                                                          row["detalhamento"].ToString());

                    dados.Add(atendimento);
                }
                return dados;

            }
            else
                return null;
        }

        internal List<Models.AtendimentoRP> TableToListRP(DataTable dt)
        {

            List<Models.AtendimentoRP> dados = new List<Models.AtendimentoRP>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {

                    int prot = Convert.ToInt32(row["codigoprotocolo"]);
                    Models.Protocolo protocolo = new DAL.ProtocoloDAL().ObterProtocolo(prot);

                    string teste = (row["horainicial"]).ToString();
                    TimeSpan timespan = TimeSpan.Parse(teste);
                    DateTime dataini = DateTime.Today.Add(timespan);

                    string teste2 = (row["horafinal"]).ToString();
                    TimeSpan timespan2 = TimeSpan.Parse(teste2);
                    DateTime datafim = DateTime.Today.Add(timespan2);

                    int cliente = Convert.ToInt32(row["codcliente"]);
                    Models.Cliente cli = new DAL.ClienteDAL().ObterCliente(cliente);

                    Models.AtendimentoRP atendimento = new Models.AtendimentoRP((int)row["codigo"],
                                                                          row["titulo"].ToString(),
                                                                          protocolo,
                                                                          Convert.ToDateTime(row["data"]),
                                                                          dataini,
                                                                          datafim,
                                                                          row["detalhamento"].ToString(),
                                                                          cli);

                    dados.Add(atendimento);
                }
                return dados;

            }
            else
                return null;
        }

        internal Boolean GravarAtendimento(Models.Atendimento Atendimento
    )
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", Atendimento.Codigo);
            b.getComandoSQL().Parameters.AddWithValue("@titulo", Atendimento.Titulo);
            b.getComandoSQL().Parameters.AddWithValue("@codigoprotocolo", Atendimento.Protocolo.Cod);
            b.getComandoSQL().Parameters.AddWithValue("@codigoprocesso", Atendimento.Codigoprocesso);
            b.getComandoSQL().Parameters.AddWithValue("@data", Atendimento.Data);
            b.getComandoSQL().Parameters.AddWithValue("@horainicial", Atendimento.Horainicial);
            b.getComandoSQL().Parameters.AddWithValue("@horafinal", Atendimento.Horafinal);
            b.getComandoSQL().Parameters.AddWithValue("@detalhamento", Atendimento.Detalhamento);



            b.getComandoSQL().CommandText = @"insert into atendimento (codigo,titulo,codigoprotocolo,codigoprocesso,data,horainicial,horafinal,detalhamento) 
                                                                               values(@codigo,@titulo,@codigoprotocolo,@codigoprocesso,@data,@horainicial,@horafinal,@detalhamento)";




            return b.ExecutaComando() == 1;
        }

        internal Boolean AlterarAtendimento(Models.Atendimento Atendimento)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", Atendimento.Codigo);
            b.getComandoSQL().Parameters.AddWithValue("@titulo", Atendimento.Titulo);
            b.getComandoSQL().Parameters.AddWithValue("@codigoprotocolo", Atendimento.Protocolo.Cod);
            b.getComandoSQL().Parameters.AddWithValue("@codigoprocesso", Atendimento.Codigoprocesso);
            b.getComandoSQL().Parameters.AddWithValue("@data", Atendimento.Data);
            b.getComandoSQL().Parameters.AddWithValue("@horainicial", Atendimento.Horainicial);
            b.getComandoSQL().Parameters.AddWithValue("@horafinal", Atendimento.Horafinal);
            b.getComandoSQL().Parameters.AddWithValue("@detalhamento", Atendimento.Detalhamento);



            b.getComandoSQL().CommandText = @"update atendimento  set
                                            titulo = @titulo,
                                            codigoprotocolo = @codigoprotocolo,   
                                            codigoprocesso = @codigoprocesso,
                                            data = @data,
                                            horainicial = @horainicial,
                                            horafinal = @horafinal,
                                            detalhamento = @detalhamento 

                                            where codigo = @codigo";





            return b.ExecutaComando() == 1;
        }

        internal List<Models.Atendimento> ObterAtendimentos(int cod)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigoprotocolo", cod);

            b.getComandoSQL().CommandText = @"select codigo,titulo,codigoprotocolo,codigoprocesso,data,horainicial,horafinal,detalhamento
                                              from atendimento
                                              where codigoprotocolo = @codigoprotocolo";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.AtendimentoRP> ObterAtendimentosRP(int cliente, DateTime? datai, DateTime? dataf, int protocolo)
        {
            b.getComandoSQL().Parameters.Clear();


            if(protocolo == 0)
            {

                if (cliente == 0)
                {
                    if (datai == dataf)
                    {

                        if (datai != null)
                        {
                            b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                            b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);
                            b.getComandoSQL().CommandText = @"select a.codigo, a.titulo, a.codigoprotocolo, a.codigoprocesso, a.data, a.horainicial, a.horafinal,a. detalhamento, p.codcliente
                                                        from atendimento a
                                                        inner join protocolo p on p.codigo = a.codigoprotocolo and
                                                        DATEDIFF (data,@datai)=0
                                                        order by codigoprotocolo";
                        }
                        else
                        {
                            b.getComandoSQL().CommandText = @"select a.codigo, a.titulo, a.codigoprotocolo, a.codigoprocesso, a.data, a.horainicial, a.horafinal,a. detalhamento, p.codcliente
                                                        from atendimento a
                                                        inner join protocolo p on a.codigoprotocolo = p.codigo 
                                                        order by codigoprotocolo";

                        }



                    }
                    else
                    {
                        if (datai != null && dataf == null)
                        {
                            b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                            b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);
                            b.getComandoSQL().CommandText = @"select a.codigo, a.titulo, a.codigoprotocolo, a.codigoprocesso, a.data, a.horainicial, a.horafinal,a. detalhamento, p.codcliente
                                                        from atendimento a
                                                        inner join protocolo p on p.codigo = a.codigoprotocolo and
                                                        DATEDIFF (data,@datai)=0
                                                        order by codigoprotocolo";
                        }
                        else
                        {
                            if (datai == null || dataf == null)
                            {
                                b.getComandoSQL().CommandText = @"select a.codigo, a.titulo, a.codigoprotocolo, a.codigoprocesso, a.data, a.horainicial, a.horafinal,a. detalhamento, p.codcliente
                                                        from atendimento a
                                                        inner join protocolo p on p.codigo = a.codigoprotocolo
                                                        order by codigoprotocolo";
                            }
                            else
                            {

                                b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                                b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);
                                b.getComandoSQL().CommandText = @"select a.codigo, a.titulo, a.codigoprotocolo, a.codigoprocesso, a.data, a.horainicial, a.horafinal,a. detalhamento, p.codcliente
                                                        from atendimento a
                                                        inner join protocolo p on p.codigo = a.codigoprotocolo and
                                                        data between @datai and @dataf
                                                        order by codigoprotocolo";
                            }
                        }



                    }
                }
                else
                {
                    b.getComandoSQL().Parameters.AddWithValue("@cliente", cliente);

                    if (datai == dataf)
                    {

                        if (datai != null)
                        {
                            b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                            b.getComandoSQL().CommandText = @"select a.codigo, a.titulo, a.codigoprotocolo, a.codigoprocesso, a.data, a.horainicial, a.horafinal,a. detalhamento, p.codcliente
                                                        from atendimento a
                                                        inner join protocolo p on p.codigo = a.codigoprotocolo and
                                                        p.codcliente = @cliente and
                                                        DATEDIFF (data,@datai)=0 
                                                        order by codigoprotocolo";
                        }
                        else
                        {
                            b.getComandoSQL().CommandText = @"select a.codigo, a.titulo, a.codigoprotocolo, a.codigoprocesso, a.data, a.horainicial, a.horafinal,a. detalhamento, p.codcliente
                                                        from atendimento a
                                                        inner join protocolo p on p.codigo = a.codigoprotocolo and
                                                        p.codcliente = @cliente 
                                                        order by codigoprotocolo";

                        }
                    }
                    else
                    {
                        if (datai == null || dataf == null)
                        {


                            b.getComandoSQL().CommandText = @"select a.codigo, a.titulo, a.codigoprotocolo, a.codigoprocesso, a.data, a.horainicial, a.horafinal,a. detalhamento, p.codcliente
                                                        from atendimento a
                                                        inner join protocolo p on p.codigo = a.codigoprotocolo and
                                                        p.codcliente = @cliente 
                                                        order by codigoprotocolo";
                        }
                        else
                        {
                            b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                            b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);
                            b.getComandoSQL().CommandText = @"select a.codigo,a.titulo,a.codigoprotocolo,a.codigoprocesso,a.data,a.horainicial,a.horafinal, a.detalhamento
                                                        from atendimento a
                                                        inner join protocolo p on p.codigo = a.codigoprotocolo and
                                                        p.codcliente = @cliente and
                                                        data between @datai and @dataf
                                                        order by codigoprotocolo";
                        }

                    }


                }
            }
            else
            {
                b.getComandoSQL().Parameters.AddWithValue("@protocolo", protocolo);

                if (cliente == 0)
                {
                    if (datai == dataf)
                    {

                        if (datai != null)
                        {
                            b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                            b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);
                            b.getComandoSQL().CommandText = @"select a.codigo, a.titulo, a.codigoprotocolo, a.codigoprocesso, a.data, a.horainicial, a.horafinal,a. detalhamento, p.codcliente
                                                        from atendimento a
                                                        inner join protocolo p on p.codigo = a.codigoprotocolo and
                                                        DATEDIFF (data,@datai)=0 and
                                                        a.codigoprotocolo = @protocolo
                                                        order by codigoprotocolo";
                        }
                        else
                        {
                            b.getComandoSQL().CommandText = @"select a.codigo, a.titulo, a.codigoprotocolo, a.codigoprocesso, a.data, a.horainicial, a.horafinal,a. detalhamento, p.codcliente
                                                        from atendimento a
                                                        inner join protocolo p on a.codigoprotocolo = p.codigo and
                                                        a.codigoprotocolo = @protocolo
                                                        order by codigoprotocolo";

                        }



                    }
                    else
                    {
                        if (datai != null && dataf == null)
                        {
                            b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                            b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);
                            b.getComandoSQL().CommandText = @"select a.codigo, a.titulo, a.codigoprotocolo, a.codigoprocesso, a.data, a.horainicial, a.horafinal,a. detalhamento, p.codcliente
                                                        from atendimento a
                                                        inner join protocolo p on p.codigo = a.codigoprotocolo and
                                                        DATEDIFF (data,@datai)=0 and
                                                        a.codigoprotocolo = @protocolo
                                                        order by codigoprotocolo";
                        }
                        else
                        {
                            if (datai == null || dataf == null)
                            {
                                b.getComandoSQL().CommandText = @"select a.codigo, a.titulo, a.codigoprotocolo, a.codigoprocesso, a.data, a.horainicial, a.horafinal,a. detalhamento, p.codcliente
                                                        from atendimento a
                                                        inner join protocolo p on p.codigo = a.codigoprotocolo and
                                                        a.codigoprotocolo = @protocolo
                                                        order by codigoprotocolo";
                            }
                            else
                            {

                                b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                                b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);
                                b.getComandoSQL().CommandText = @"select a.codigo, a.titulo, a.codigoprotocolo, a.codigoprocesso, a.data, a.horainicial, a.horafinal,a. detalhamento, p.codcliente
                                                        from atendimento a
                                                        inner join protocolo p on p.codigo = a.codigoprotocolo and
                                                        data between @datai and @dataf and
                                                        a.codigoprotocolo = @protocolo
                                                        order by codigoprotocolo";
                            }
                        }



                    }
                }
                else
                {
                    b.getComandoSQL().Parameters.AddWithValue("@cliente", cliente);

                    if (datai == dataf)
                    {

                        if (datai != null)
                        {
                            b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                            b.getComandoSQL().CommandText = @"select a.codigo, a.titulo, a.codigoprotocolo, a.codigoprocesso, a.data, a.horainicial, a.horafinal,a. detalhamento, p.codcliente
                                                        from atendimento a
                                                        inner join protocolo p on p.codigo = a.codigoprotocolo and
                                                        p.codcliente = @cliente and
                                                        DATEDIFF (data,@datai)=0 and
                                                        a.codigoprotocolo = @protocolo
                                                        order by codigoprotocolo";
                        }
                        else
                        {
                            b.getComandoSQL().CommandText = @"select a.codigo, a.titulo, a.codigoprotocolo, a.codigoprocesso, a.data, a.horainicial, a.horafinal,a. detalhamento, p.codcliente
                                                        from atendimento a
                                                        inner join protocolo p on p.codigo = a.codigoprotocolo and
                                                        p.codcliente = @cliente and
                                                        a.codigoprotocolo = @protocolo
                                                        order by codigoprotocolo";

                        }
                    }
                    else
                    {
                        if (datai == null || dataf == null)
                        {


                            b.getComandoSQL().CommandText = @"select a.codigo, a.titulo, a.codigoprotocolo, a.codigoprocesso, a.data, a.horainicial, a.horafinal,a. detalhamento, p.codcliente
                                                        from atendimento a
                                                        inner join protocolo p on p.codigo = a.codigoprotocolo and
                                                        p.codcliente = @cliente and
                                                        a.codigoprotocolo = @protocolo
                                                        order by codigoprotocolo";
                        }
                        else
                        {
                            b.getComandoSQL().Parameters.AddWithValue("@datai", datai);
                            b.getComandoSQL().Parameters.AddWithValue("@dataf", dataf);
                            b.getComandoSQL().CommandText = @"select a.codigo,a.titulo,a.codigoprotocolo,a.codigoprocesso,a.data,a.horainicial,a.horafinal, a.detalhamento
                                                        from atendimento a
                                                        inner join protocolo p on p.codigo = a.codigoprotocolo and
                                                        p.codcliente = @cliente and
                                                        data between @datai and @dataf and
                                                        a.codigoprotocolo = @protocolo
                                                        order by codigoprotocolo";
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


       

        internal List<Models.Atendimento> ValidaAtendimento(int codprotocolo, DateTime diaatend, DateTime horainicio, DateTime horafim)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigoprotocolo", codprotocolo);
            b.getComandoSQL().Parameters.AddWithValue("@data", diaatend);
            b.getComandoSQL().Parameters.AddWithValue("@horainicial", horainicio);
            b.getComandoSQL().Parameters.AddWithValue("@horafinal", horafim);

            b.getComandoSQL().CommandText = @"select codigo,titulo,codigoprotocolo,codigoprocesso,data,horainicial,horafinal,detalhamento
                                              from atendimento
                                              where codigoprotocolo = @codigoprotocolo          and 
                                                               data = @data                     and
                                              (@horainicial between   horainicial and horafinal  or
                                              @horafinal   between   horainicial and horafinal) or 
                                              (horainicial between   @horainicial and @horafinal  or
                                              horafinal   between   @horainicial and @horafinal)";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal bool VerificaAtendimentos (int cod)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigoprotocolo", cod);

            b.getComandoSQL().CommandText = @"select codigo,titulo,codigoprotocolo,codigoprocesso,data,horainicial,horafinal,detalhamento
                                              from atendimento
                                              where codigoprotocolo = @codigoprotocolo";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return true;
            else
                return false;
        }


        internal bool Excluir(int cod)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", cod);

            b.getComandoSQL().CommandText = @"delete from atendimento
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }
    }
}
