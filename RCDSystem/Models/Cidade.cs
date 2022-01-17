using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Models
{
    public class Cidade
    {
        private int _cod;
        private string _descricao;
        private int _estado;

        public Cidade()
        {

        }

        public Cidade(int cod, string descricao,int estado)
        {
            _cod = cod;
            _descricao = descricao;
            _estado = estado;

        }
        public Cidade(int cod, string descricao)
        {
            _cod = cod;
            _descricao = descricao;
           

        }

        public Cidade(int cod)
        {
            _cod = cod;

        }

        public int Cod { get => _cod; set => _cod = value; }
        public string Descricao { get => _descricao; set => _descricao = value; }
        public int Estado { get => _estado; set => _estado = value; }
    }
}
