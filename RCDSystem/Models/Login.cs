using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Models
{
    public class Login
    {

        private int _codigo;
        private String _usuario;
        private String _senha;
        private bool _ativo;

        public Login(int codigo, string usuario, string senha, bool ativo)
        {
            _codigo = codigo;
            _usuario = usuario;
            _senha = senha;
            _ativo = ativo;
        }
        public Login()
        {

        }

        public int Codigo { get => _codigo; set => _codigo = value; }
        public string Usuario { get => _usuario; set => _usuario = value; }
        public string Senha { get => _senha; set => _senha = value; }
        public bool Ativo { get => _ativo; set => _ativo = value; }
    }
}
