using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controllers
{
    public class CidadeController
    {
        public List<Models.Cidade> ObterCidades(int id)
        {
            Controls.CidadeControl control = new Controls.CidadeControl();

            return control.ObterCidades(id);


           
        }
    }
}
