using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Models
{
    public class TipoCliente
    {
        private int _cod_tipo;
        private string _descricao;

        public TipoCliente()
        {

        }

        public TipoCliente (int cod_tipo, string descricao)
        {
            _cod_tipo = cod_tipo;
            _descricao = descricao;

        }

        public int Cod_tipo { get => _cod_tipo; set => _cod_tipo = value; }
        public string Descricao { get => _descricao; set => _descricao = value; }
    }
}
