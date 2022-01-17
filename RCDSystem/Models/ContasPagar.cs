using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Models
{
    public class ContasPagar
    {
        private int _codigo;
        private int _contamae;
        private DateTime _datavenc;
        private String _descricao;
        private String _detalhes;
        private bool _pago;
        private bool _possuiparcela;
        private TipoDespesa _tipodespesa;
        private decimal valor;
        private decimal _valorpago;

        public ContasPagar()
        {

        }

        public ContasPagar(int codigo, DateTime datavenc, string descricao, string detalhes, TipoDespesa tipodespesa, decimal valor)
        {
            _codigo = codigo;
            _datavenc = datavenc;
            _descricao = descricao;
            _detalhes = detalhes;
            _tipodespesa = tipodespesa;
            this.valor = valor;
        }
        public ContasPagar(int codigo, DateTime datavenc, string descricao, string detalhes, bool pago, TipoDespesa tipodespesa, decimal valor)
        {
            _codigo = codigo;
            _datavenc = datavenc;
            _descricao = descricao;
            _detalhes = detalhes;
            _pago = pago;
            _tipodespesa = tipodespesa;
            this.valor = valor;
        }

        public ContasPagar(int codigo, int contamae, DateTime datavenc, string descricao, string detalhes, bool pago, TipoDespesa tipodespesa, decimal valor, decimal valorpago)
        {
            _codigo = codigo;
            _contamae = contamae;
            _datavenc = datavenc;
            _descricao = descricao;
            _detalhes = detalhes;
            _pago = pago;
            _tipodespesa = tipodespesa;
            this.valor = valor;
            _valorpago = valorpago;
        }

        public ContasPagar(int codigo, int contamae, DateTime datavenc, string descricao, string detalhes, bool pago, bool possuiparcela, TipoDespesa tipodespesa, decimal valor, decimal valorpago)
        {
            _codigo = codigo;
            _contamae = contamae;
            _datavenc = datavenc;
            _descricao = descricao;
            _detalhes = detalhes;
            _pago = pago;
            _possuiparcela = possuiparcela;
            _tipodespesa = tipodespesa;
            this.valor = valor;
            _valorpago = valorpago;
        }

        public ContasPagar(DateTime datavenc, string descricao, string detalhes, bool pago, TipoDespesa tipodespesa, decimal valor)
        {
            _datavenc = datavenc;
            _descricao = descricao;
            _detalhes = detalhes;
            _pago = pago;
            _tipodespesa = tipodespesa;
            this.valor = valor;
        }

        public int Codigo { get => _codigo; set => _codigo = value; }
        public int Contamae { get => _contamae; set => _contamae = value; }
        public DateTime Datavenc { get => _datavenc; set => _datavenc = value; }
        public string Descricao { get => _descricao; set => _descricao = value; }
        public string Detalhes { get => _detalhes; set => _detalhes = value; }
        public bool Pago { get => _pago; set => _pago = value; }
        public bool Possuiparcela { get => _possuiparcela; set => _possuiparcela = value; }
        public TipoDespesa Tipodespesa { get => _tipodespesa; set => _tipodespesa = value; }
        public decimal Valor { get => valor; set => valor = value; }
        public decimal Valorpago { get => _valorpago; set => _valorpago = value; }
    }
}
