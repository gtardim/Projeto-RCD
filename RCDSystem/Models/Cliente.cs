namespace RCDSystem.Models
{
    public class Cliente
    {
        private int _cod;
        private string _nome;
        private string _nacionalidade;
        private string _estadocivil;
        private string _RG;
        private string _CPF;
        private string _CNPJ;
        private string _rua;
        private string _bairro;
        private Cidade _cidade;
        private string _numero;
        private string _cep;
        private string _email;
        private string _contato;
        private string _observacoes;
        private int _representante;
        private int _tipocliente;
        // adiado provisoriamente private byte _foto;

        public int Cod { get => _cod; set => _cod = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public string Nacionalidade { get => _nacionalidade; set => _nacionalidade = value; }
        public string Estadocivil { get => _estadocivil; set => _estadocivil = value; }
        public string RG { get => _RG; set => _RG = value; }
        public string CPF { get => _CPF; set => _CPF = value; }
        public string CNPJ { get => _CNPJ; set => _CNPJ = value; }
        public string Rua { get => _rua; set => _rua = value; }
        public string Bairro { get => _bairro; set => _bairro = value; }
        public Cidade Cidade { get => _cidade; set => _cidade = value; }
        public string Numero { get => _numero; set => _numero = value; }
        public string Cep { get => _cep; set => _cep = value; }
        public string Email { get => _email; set => _email = value; }
        public string Contato { get => _contato; set => _contato = value; }
        public string Observacoes { get => _observacoes; set => _observacoes = value; }
        public int Representante { get => _representante; set => _representante = value; }
        public int Tipocliente { get => _tipocliente; set => _tipocliente = value; }

        

        public Cliente()
        {

        }

        public Cliente(int cod, string nome, string nacionalidade, string estadocivil, string rG, string cPF, string cNPJ, string rua, string bairro, Cidade cidade, string numero, string cep, string email, string contato, string observacoes, int representante, int tipocliente)
        {
            _cod = cod;
            _nome = nome;
            _nacionalidade = nacionalidade;
            _estadocivil = estadocivil;
            _RG = rG;
            _CPF = cPF;
            _CNPJ = cNPJ;
            _rua = rua;
            _bairro = bairro;
            _cidade = cidade;
            _numero = numero;
            _cep = cep;
            _email = email;
            _contato = contato;
            _observacoes = observacoes;
            _representante = representante;
            _tipocliente = tipocliente;
        }

        public Cliente(int cod, string nome, string nacionalidade, string estadocivil, string rG, string cPF, string cNPJ, string rua, string bairro, string numero, string cep, string email, string contato, string observacoes, int tipocliente)
        {
            _cod = cod;
            _nome = nome;
            _nacionalidade = nacionalidade;
            _estadocivil = estadocivil;
            _RG = rG;
            _CPF = cPF;
            _CNPJ = cNPJ;
            _rua = rua;
            _bairro = bairro;
            _numero = numero;
            _cep = cep;
            _email = email;
            _contato = contato;
            _observacoes = observacoes;
            _tipocliente = tipocliente;
        }

        public Cliente(int cod, string nome, string nacionalidade, string estadocivil, string rG, string cPF, string cNPJ, string rua, string bairro,Cidade cidade, string numero, string cep, string email, string contato, int tipocliente)
        {
            _cod = cod;
            _nome = nome;
            _nacionalidade = nacionalidade;
            _estadocivil = estadocivil;
            _RG = rG;
            _CPF = cPF;
            _CNPJ = cNPJ;
            _rua = rua;
            _bairro = bairro;
            _cidade = cidade;
            _numero = numero;
            _cep = cep;
            _email = email;
            _contato = contato;
            _tipocliente = tipocliente;
        }
    }
}
