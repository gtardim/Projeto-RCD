using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controls
{
    public class EstadoControl
    {
        public List<Models.Estado> ObterTodos()
        {
            DAL.EstadoDAL dal = new DAL.EstadoDAL();

            return dal.ObterTodos();
        }

    }
}
