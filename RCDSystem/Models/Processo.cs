using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Models
{
    public class Processo
    {

        private int _cod;
        private string _descricao;
        private Cliente _cliente;
        private CategoriaPP _categoria;
        private StatusPP _status;
        private string _numregistro;
        private decimal _valortotal;
        private string _observacoes;


        public Processo()
        {

        }
        public Processo(int cod, string descricao, Cliente cliente, CategoriaPP categoria, StatusPP status, string numregistro, decimal valortotal, string observacoes)
        {
            _cod = cod;
            _descricao = descricao;
            _cliente = cliente;
            _categoria = categoria;
            _status = status;
            _numregistro = numregistro;
            _valortotal = valortotal;
            _observacoes = observacoes;
        }

        public int Cod { get => _cod; set => _cod = value; }
        public string Descricao { get => _descricao; set => _descricao = value; }
        public Cliente Cliente { get => _cliente; set => _cliente = value; }
        public CategoriaPP Categoria { get => _categoria; set => _categoria = value; }
        public StatusPP Status { get => _status; set => _status = value; }
        public string Numregistro { get => _numregistro; set => _numregistro = value; }
        public decimal Valortotal { get => _valortotal; set => _valortotal = value; }
        public string Observacoes { get => _observacoes; set => _observacoes = value; }
    }
}
