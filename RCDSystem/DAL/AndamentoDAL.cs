using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.DAL
{
    public class AndamentoDAL
    {

        private Banco b;
        internal AndamentoDAL()
        {
            b = Banco.GetInstance();
        }

        internal Boolean AlterarProcesso(int codigo,int novostatus)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo",codigo);
            b.getComandoSQL().Parameters.AddWithValue("@status", novostatus);

            b.getComandoSQL().CommandText = @"update processo  set
                                                  status = @status
                                            where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }

        internal Boolean AlterarProtocolo(int codigo, int novostatus)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", codigo);
            b.getComandoSQL().Parameters.AddWithValue("@status", novostatus);

            b.getComandoSQL().CommandText = @"update protocolo  set
                                                  status = @status
                                            where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }
    }
}
