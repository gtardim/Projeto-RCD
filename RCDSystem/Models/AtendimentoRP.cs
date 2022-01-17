using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Models
{
    public class AtendimentoRP
    {
        private int _codigo;
        private String _titulo;
        private Protocolo _protocolo;
        private int _codigoprocesso;
        private DateTime _data;
        private DateTime _horainicial;
        private DateTime _horafinal;
        private String _detalhamento;
        private Cliente _cliente;

        public AtendimentoRP(int codigo, string titulo, Protocolo protocolo, int codigoprocesso, DateTime data, DateTime horainicial, DateTime horafinal, string detalhamento, Cliente cliente)
        {
            _codigo = codigo;
            _titulo = titulo;
            _protocolo = protocolo;
            _codigoprocesso = codigoprocesso;
            _data = data;
            _horainicial = horainicial;
            _horafinal = horafinal;
            _detalhamento = detalhamento;
            _cliente = cliente;
        }
        public AtendimentoRP(int codigo, string titulo, Protocolo protocolo, DateTime data, DateTime horainicial, DateTime horafinal, string detalhamento, Cliente cliente)
        {
            _codigo = codigo;
            _titulo = titulo;
            _protocolo = protocolo;
            _data = data;
            _horainicial = horainicial;
            _horafinal = horafinal;
            _detalhamento = detalhamento;
            _cliente = cliente;
        }

        public int Codigo { get => _codigo; set => _codigo = value; }
        public string Titulo { get => _titulo; set => _titulo = value; }
        public Protocolo Protocolo { get => _protocolo; set => _protocolo = value; }
        public int Codigoprocesso { get => _codigoprocesso; set => _codigoprocesso = value; }
        public DateTime Data { get => _data; set => _data = value; }
        public DateTime Horainicial { get => _horainicial; set => _horainicial = value; }
        public DateTime Horafinal { get => _horafinal; set => _horafinal = value; }
        public string Detalhamento { get => _detalhamento; set => _detalhamento = value; }
        public Cliente Cliente { get => _cliente; set => _cliente = value; }
    }
}
