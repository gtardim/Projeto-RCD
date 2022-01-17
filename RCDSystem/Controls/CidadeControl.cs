using RCDSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controls
{
    public class CidadeControl
    {
        public List<Cidade> ObterCidades(int id)
        {
            DAL.CidadeDAL dal = new DAL.CidadeDAL();

            return dal.ObterCidades(id);
        }
    }
}
