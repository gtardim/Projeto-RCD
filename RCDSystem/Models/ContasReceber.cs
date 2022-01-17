using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Models
{
    public class ContasReceber
    {

        private int _codigo;
        private int _contamae;
        private int _origem;
        private int _tipo;
        private String _descricao;
        private Cliente _cliente;
        private decimal valor;
        private decimal _valorpago;
        private DateTime _datageracao;
        private DateTime? _pago;
        private bool _possuiparcela;

        public int Codigo { get => _codigo; set => _codigo = value; }
        public int Contamae { get => _contamae; set => _contamae = value; }
        public int Origem { get => _origem; set => _origem = value; }
        public int Tipo { get => _tipo; set => _tipo = value; }
        public string Descricao { get => _descricao; set => _descricao = value; }
        public Cliente Cliente { get => _cliente; set => _cliente = value; }
        public decimal Valor { get => valor; set => valor = value; }
        public decimal Valorpago { get => _valorpago; set => _valorpago = value; }
        public DateTime Datageracao { get => _datageracao; set => _datageracao = value; }
        public DateTime? Pago { get => _pago; set => _pago = value; }
        public bool Possuiparcela { get => _possuiparcela; set => _possuiparcela = value; }

        public ContasReceber()
        {

        }

        public ContasReceber(int codigo, int contamae, int origem, int tipo, string descricao, Cliente cliente, decimal valor, decimal valorpago, DateTime datageracao, DateTime? pago, bool possuiparcela)
        {
            _codigo = codigo;
            _contamae = contamae;
            _origem = origem;
            _tipo = tipo;
            _descricao = descricao;
            _cliente = cliente;
            this.valor = valor;
            _valorpago = valorpago;
            _datageracao = datageracao;
            _pago = pago;
            _possuiparcela = possuiparcela;
        }

        public ContasReceber(int codigo, int contamae, int origem, int tipo, string descricao, Cliente cliente, decimal valor, decimal valorpago, DateTime datageracao, bool possuiparcela)
        {
            _codigo = codigo;
            _contamae = contamae;
            _origem = origem;
            _tipo = tipo;
            _descricao = descricao;
            _cliente = cliente;
            this.valor = valor;
            _valorpago = valorpago;
            _datageracao = datageracao;
            _possuiparcela = possuiparcela;
        }


    }
}
