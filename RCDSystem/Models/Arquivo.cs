using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Models
{
    public class Arquivo
    {
        private int _cod;
        private Cliente cli;
        private int _processocod;
        private TipoArquivo _tipoarq;
        private String _descricao;
        private byte[] _arq;
        private DateTime _DataInclusao;
        private String _nome;
        private String _formato;
        private String _tipo;
        private int tamanho;


        public Arquivo()
        {

        }
        public Arquivo(int cod, Cliente cli, int processocod, TipoArquivo tipoarq, string descricao, byte[] arq, DateTime dataInclusao, string nome, string formato, string tipo, int tamanho)
        {
            _cod = cod;
            this.cli = cli;
            _processocod = processocod;
            _tipoarq = tipoarq;
            _descricao = descricao;
            _arq = arq;
            _DataInclusao = dataInclusao;
            _nome = nome;
            _formato = formato;
            _tipo = tipo;
            this.tamanho = tamanho;
        }

        public Arquivo(byte[] arq, string tipo, string formato, string nome, int tamanho)
        {
            _arq = arq;  
            _nome = nome;
            _formato = formato;
            _tipo = tipo;
            this.tamanho = tamanho;
        }

        public Arquivo(byte[] arq,string tipo, string formato,String nome)
        {
            _arq = arq;
            _tipo = tipo;
            _formato = formato;
            _nome = nome;
        }

        public Arquivo( Cliente cli, int processocod, TipoArquivo tipoarq, string descricao, byte[] arq, DateTime dataInclusao, string nome, string formato, string tipo, int tamanho)
        {
            
            this.cli = cli;
            _processocod = processocod;
            _tipoarq = tipoarq;
            _descricao = descricao;
            _arq = arq;
            _DataInclusao = dataInclusao;
            _nome = nome;
            _formato = formato;
            _tipo = tipo;
            this.tamanho = tamanho;
        }

        public int Cod { get => _cod; set => _cod = value; }
        public Cliente Cli { get => cli; set => cli = value; }
        public int Processocod { get => _processocod; set => _processocod = value; }
        public TipoArquivo Tipoarq { get => _tipoarq; set => _tipoarq = value; }
        public string Descricao { get => _descricao; set => _descricao = value; }
        public byte[] Arq { get => _arq; set => _arq = value; }
        public DateTime DataInclusao { get => _DataInclusao; set => _DataInclusao = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public string Formato { get => _formato; set => _formato = value; }
        public string Tipo { get => _tipo; set => _tipo = value; }
        public int Tamanho { get => tamanho; set => tamanho = value; }
    }
}
