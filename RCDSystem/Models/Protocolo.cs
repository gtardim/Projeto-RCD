using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Models
{
    public class Protocolo
    {
        private int _cod;
        private string _descricao;
        private Cliente _cliente;
        private CategoriaPP _categoria;
        private StatusPP _status;
        private decimal _valortotal;
        private int _processo;
        private string _observacoes;

        public Protocolo(int cod, string descricao, Cliente cliente, CategoriaPP categoria, StatusPP status, decimal valortotal, int processo, string observacoes)
        {
            _cod = cod;
            _descricao = descricao;
            _cliente = cliente;
            _categoria = categoria;
            _status = status;
            _valortotal = valortotal;
            _processo = processo;
            _observacoes = observacoes;
        }

        public Protocolo(int cod, string descricao, Cliente cliente, CategoriaPP categoria, StatusPP status, decimal valortotal, string observacoes)
        {
            _cod = cod;
            _descricao = descricao;
            _cliente = cliente;
            _categoria = categoria;
            _status = status;
            _valortotal = valortotal;
            _observacoes = observacoes;
        }

        public Protocolo(string descricao, Cliente cliente, CategoriaPP categoria, StatusPP status, decimal valortotal, int processo, string observacoes)
        {
            _descricao = descricao;
            _cliente = cliente;
            _categoria = categoria;
            _status = status;
            _valortotal = valortotal;
            _processo = processo;
            _observacoes = observacoes;
        }

        public int Cod { get => _cod; set => _cod = value; }
        public string Descricao { get => _descricao; set => _descricao = value; }
        public Cliente Cliente { get => _cliente; set => _cliente = value; }
        public CategoriaPP Categoria { get => _categoria; set => _categoria = value; }
        public StatusPP Status { get => _status; set => _status = value; }
        public decimal Valortotal { get => _valortotal; set => _valortotal = value; }
        public int Processo { get => _processo; set => _processo = value; }
        public string Observacoes { get => _observacoes; set => _observacoes = value; }
    }
}
