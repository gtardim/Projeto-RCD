using RCDSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controls
{
    public class ClienteControl
    {


        public bool Gravar(string nome, string nacionalidade, string estadocivil, string RG, string CPFCNPJ, string rua, string bairro, string numero, int cidade, string CEP, string email, string contato, int representante, bool chekCPF)
        {
            string CPF = "";
            string CNPJ = "";
            int tipocliente = 0;

            if (chekCPF == true)
            {
                CPF = CPFCNPJ;
                CNPJ = "";
                tipocliente = 1;
            }
            else
            {
                CNPJ = CPFCNPJ;
                CPF = "";
                tipocliente = 2;
            }
            

            Cidade cid = new DAL.CidadeDAL().ObterCidade(cidade);

            Cliente cli = new Cliente(0, nome, nacionalidade, estadocivil, RG, CPF, CNPJ, rua, bairro, cid, numero, CEP, email, contato, tipocliente);

            DAL.ClienteDAL dal = new DAL.ClienteDAL();

            return dal.Gravar(cli);

        }

        public List<Cliente> ObterTodos()
        {
            DAL.ClienteDAL dal = new DAL.ClienteDAL();

            return dal.ObterTodos();
        }

        public Cliente ObterCliente(int cod)
        {
            DAL.ClienteDAL dal = new DAL.ClienteDAL();

            return dal.ObterCliente(cod);
        }

        public bool Excluir(int cod)
        {

            DAL.ClienteDAL dal = new DAL.ClienteDAL();

            return dal.Excluir(cod);
        }

        public bool Alterar(int cod ,string nome, string nacionalidade, string estadocivil, string RG, string CPFCNPJ, string rua, string bairro, string numero, int cidade, string CEP, string email, string contato, int representante, bool editchekCPF)
        {

            string CPF = "";
            string CNPJ = "";
            int tipocliente = 0;

            if (editchekCPF == true)
            {
                CPF = CPFCNPJ;
                CNPJ = "";
                tipocliente = 1;
            }
            else
            {
                CNPJ = CPFCNPJ;
                CPF = "";
                tipocliente = 2;
            }


            Cidade cid = new DAL.CidadeDAL().ObterCidade(cidade);

            Cliente cli = new Cliente(cod, nome, nacionalidade, estadocivil, RG, CPF, CNPJ, rua, bairro, cid, numero, CEP, email, contato, tipocliente);

            DAL.ClienteDAL dal = new DAL.ClienteDAL();

            return dal.Alterar(cli);
        }


    }
}
