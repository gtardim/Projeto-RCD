using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Models
{
    public class ProtocoloReport
    {

        private int _cod;
        private string _descricao;
        private Cliente _cliente;
        private CategoriaPP _categoria;
        private StatusPP _status;
        private decimal _valortotal;
        private int _processo;
        private string _observacoes;
        private DateTime _criacao;

        public ProtocoloReport(int cod, string descricao, Cliente cliente, CategoriaPP categoria, StatusPP status, decimal valortotal, int processo, string observacoes, DateTime criacao)
        {
            _cod = cod;
            _descricao = descricao;
            _cliente = cliente;
            _categoria = categoria;
            _status = status;
            _valortotal = valortotal;
            _processo = processo;
            _observacoes = observacoes;
            _criacao = criacao;
        }

        public int Cod { get => _cod; set => _cod = value; }
        public string Descricao { get => _descricao; set => _descricao = value; }
        public Cliente Cliente { get => _cliente; set => _cliente = value; }
        public CategoriaPP Categoria { get => _categoria; set => _categoria = value; }
        public StatusPP Status { get => _status; set => _status = value; }
        public decimal Valortotal { get => _valortotal; set => _valortotal = value; }
        public int Processo { get => _processo; set => _processo = value; }
        public string Observacoes { get => _observacoes; set => _observacoes = value; }
        public DateTime Criacao { get => _criacao; set => _criacao = value; }
    }
}
