using RCDSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controls
{
    public class RegistrarArquivoControl
    {

        public bool Gravar(int cliente,int numprocesso, int tipoarq, String descricao, Byte[] arq,String nome,String formato,String tipo,int tamanho)
        {
            
            Cliente cli = new DAL.ClienteDAL().ObterCliente(cliente);
            TipoArquivo tipoarquivo = new DAL.TipoArquivoDAL().ObterTipo(tipoarq);

            Arquivo arquivo = new Arquivo(cli, numprocesso, tipoarquivo, descricao, arq, Convert.ToDateTime("01/01/2020"), nome, formato, tipo, tamanho);

            DAL.RegistrarArquivoDAL dal = new DAL.RegistrarArquivoDAL();

            return dal.Gravar(arquivo);
        }

        public bool GravarComProcesso(int cliente, int processo, int tipoarq, String descricao, Byte[] arq, String nome, String formato, String tipo, int tamanho)
        {

            Cliente cli = new DAL.ClienteDAL().ObterCliente(cliente);
            TipoArquivo tipoarquivo = new DAL.TipoArquivoDAL().ObterTipo(tipoarq);

            Arquivo arquivo = new Arquivo(cli,processo, tipoarquivo, descricao, arq, Convert.ToDateTime("01/01/2020"), nome, formato, tipo, tamanho);

            DAL.RegistrarArquivoDAL dal = new DAL.RegistrarArquivoDAL();

            return dal.Gravar(arquivo);
        }

        public bool Alterar(int codigo ,int cliente, int numprocesso, int tipoarq, String descricao, Byte[] arq, String nome, String formato, String tipo, int tamanho)
        {

            Cliente cli = new DAL.ClienteDAL().ObterCliente(cliente);
            TipoArquivo tipoarquivo = new DAL.TipoArquivoDAL().ObterTipo(tipoarq);

            

            Arquivo arquivo = new Arquivo(codigo,cli, numprocesso, tipoarquivo, descricao, arq, Convert.ToDateTime("01/01/2020"), nome, formato, tipo, tamanho);

            DAL.RegistrarArquivoDAL dal = new DAL.RegistrarArquivoDAL();

            return dal.Alterar(arquivo);
        }

        public List<Arquivo> ObterTodos()
        {
            DAL.RegistrarArquivoDAL dal = new DAL.RegistrarArquivoDAL();

            return dal.ObterTodos();
        }


        public Arquivo GetArquivo(int cod)
        {
            DAL.RegistrarArquivoDAL dal = new DAL.RegistrarArquivoDAL();

            return dal.GetArquivo(cod);
        }

        public Arquivo GetArquivoFull(int cod)
        {
            DAL.RegistrarArquivoDAL dal = new DAL.RegistrarArquivoDAL();

            return dal.GetArquivo(cod);
        }

        public bool Excluir(int cod)
        {

            DAL.RegistrarArquivoDAL dal = new DAL.RegistrarArquivoDAL();

            return dal.Excluir(cod);
        }

    }
}
