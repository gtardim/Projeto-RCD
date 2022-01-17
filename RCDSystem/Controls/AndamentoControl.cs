using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controls
{
    public class AndamentoControl
    {

        public bool AlterarProcesso(int codigo, int novostatus)
        {

            bool ok = false;
            DAL.AndamentoDAL dal = new DAL.AndamentoDAL();

            return dal.AlterarProcesso(codigo, novostatus);
        }

        public bool AlterarProtocolo(int codigo, int novostatus)
        {

            bool ok = false;
            DAL.AndamentoDAL dal = new DAL.AndamentoDAL();

            return dal.AlterarProtocolo(codigo, novostatus);
        }
    }
}
