using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Models
{
    public class Atendimento
    {

        private int _codigo;
        private String _titulo;
        private Protocolo _protocolo;
        private int _codigoprocesso;
        private DateTime _data;
        private DateTime _horainicial;
        private DateTime _horafinal;
        private String _detalhamento;

        public Atendimento(int codigo, string titulo, Protocolo protocolo, int codigoprocesso, DateTime data, DateTime horainicial, DateTime horafinal, string detalhamento)
        {
            _codigo = codigo;
            _titulo = titulo;
            _protocolo = protocolo;
            _codigoprocesso = codigoprocesso;
            _data = data;
            _horainicial = horainicial;
            _horafinal = horafinal;
            _detalhamento = detalhamento;
        }

        public Atendimento(int codigo, string titulo, Protocolo protocolo, DateTime data, DateTime horainicial, DateTime horafinal, string detalhamento)
        {
            _codigo = codigo;
            _titulo = titulo;
            _protocolo = protocolo;
            _data = data;
            _horainicial = horainicial;
            _horafinal = horafinal;
            _detalhamento = detalhamento;
        }

        public Atendimento(int codigo, string titulo, int codigoprocesso, DateTime data, DateTime horainicial, DateTime horafinal, string detalhamento)
        {
            _codigo = codigo;
            _titulo = titulo;
            _codigoprocesso = codigoprocesso;
            _data = data;
            _horainicial = horainicial;
            _horafinal = horafinal;
            _detalhamento = detalhamento;
        }

        public Atendimento()
        {

        }

        public int Codigo { get => _codigo; set => _codigo = value; }
        public string Titulo { get => _titulo; set => _titulo = value; }
        public Protocolo Protocolo { get => _protocolo; set => _protocolo = value; }
        public int Codigoprocesso { get => _codigoprocesso; set => _codigoprocesso = value; }
        public DateTime Data { get => _data; set => _data = value; }
        public DateTime Horainicial { get => _horainicial; set => _horainicial = value; }
        public DateTime Horafinal { get => _horafinal; set => _horafinal = value; }
        public string Detalhamento { get => _detalhamento; set => _detalhamento = value; }
    }
}
