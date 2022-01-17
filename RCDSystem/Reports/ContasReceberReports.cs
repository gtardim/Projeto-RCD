using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Reports
{
    public class ContasReceberReports : TNEReports
    {
        private int _cliente;
        private DateTime? _data_i;
        private DateTime? _data_f;

        public ContasReceberReports(int cliente, string data_i, string data_f)
        {
            Paisagem = false;
            _cliente = cliente;

            if (data_i != null)
                _data_i = Convert.ToDateTime(data_i);
            else
                _data_i = null;

            if (data_f != null)
                _data_f = Convert.ToDateTime(data_f);
            else
                _data_f = null;

        }

        public int Cliente { get => _cliente; set => _cliente = value; }
        public DateTime? Data_i { get => _data_i; set => _data_i = value; }
        public DateTime? Data_f { get => _data_f; set => _data_f = value; }

        public override void MontaCorpoDados()
        {
            base.MontaCorpoDados();


            #region Cabeçalho do Relatório
            PdfPTable table = new PdfPTable(5);
            BaseColor preto = new BaseColor(0, 0, 0);
            BaseColor fundo = new BaseColor(200, 200, 200);
            Font font = FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto);
            Font titulo = FontFactory.GetFont("Verdana", 8, Font.BOLD, preto);

            float[] colsW = { 10, 10, 10, 10, 10 };
            table.SetWidths(colsW);
            table.HeaderRows = 1;
            table.WidthPercentage = 100f;

            table.DefaultCell.Border = PdfPCell.BOTTOM_BORDER;
            table.DefaultCell.BorderColor = preto;
            table.DefaultCell.BorderColorBottom = new BaseColor(255, 255, 255);
            table.DefaultCell.Padding = 10;

            table.AddCell(getNewCell("Codigo", titulo, Element.ALIGN_LEFT, 10, PdfPCell.BOTTOM_BORDER, preto, fundo));
            table.AddCell(getNewCell("Descrição", titulo, Element.ALIGN_LEFT, 10, PdfPCell.BOTTOM_BORDER, preto, fundo));
            table.AddCell(getNewCell("Cliente", titulo, Element.ALIGN_LEFT, 10, PdfPCell.BOTTOM_BORDER, preto, fundo));
            table.AddCell(getNewCell("Data", titulo, Element.ALIGN_LEFT, 10, PdfPCell.BOTTOM_BORDER, preto, fundo));
            table.AddCell(getNewCell("Valor", titulo, Element.ALIGN_RIGHT, 10, PdfPCell.BOTTOM_BORDER, preto, fundo));

            #endregion

            PdfPCell TotalGeral;
            decimal totalprotocolo = 0;
            decimal totalprocesso = 0;

            var contas = new DAL.ProtocoloDAL().ObterRecebimentos(Cliente,Data_i,Data_f);
            if (contas != null)
            {
                table.AddCell(getNewCell("Protocolos", titulo, Element.ALIGN_LEFT, 14, PdfPCell.NO_BORDER,preto));
                table.AddCell(getNewCell("", font));
                table.AddCell(getNewCell("", font));
                table.AddCell(getNewCell("", font));
                table.AddCell(getNewCell("", font));

                table.AddCell("");
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");
                PdfPCell celltotal;

                foreach (var d in contas)
                {
                    table.AddCell(getNewCell(d.Cod.ToString(), font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                    table.AddCell(getNewCell(d.Descricao, font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                    table.AddCell(getNewCell(d.Cliente.Nome, font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                    table.AddCell(getNewCell(d.Criacao.ToString("dd/MM/yyyy"), font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                    table.AddCell(getNewCell(d.Valortotal.ToString(), font, Element.ALIGN_RIGHT, 10, PdfPCell.NO_BORDER));
                    totalprotocolo = d.Valortotal + totalprotocolo;

                }

                celltotal = getNewCell("Total Protocolo: " + totalprotocolo.ToString(), titulo, Element.ALIGN_RIGHT, 10, PdfPCell.NO_BORDER);
                celltotal.Colspan = 7;
                table.AddCell(celltotal);

            }

            var contasprocesso = new DAL.ProcessoDAL().ObterRecebimentos(Cliente, Data_i, Data_f);
            if (contasprocesso != null)
            {
                table.AddCell(getNewCell("Processos", titulo, Element.ALIGN_LEFT, 14, PdfPCell.NO_BORDER, preto));
                table.AddCell(getNewCell("",font));
                table.AddCell(getNewCell("", font));
                table.AddCell(getNewCell("", font));
                table.AddCell(getNewCell("", font));

                table.AddCell("");
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");

                PdfPCell celltotal;
                foreach (var d in contasprocesso)
                {
                    table.AddCell(getNewCell(d.Cod.ToString(), font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                    table.AddCell(getNewCell(d.Descricao, font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                    table.AddCell(getNewCell(d.Cliente.Nome, font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                    table.AddCell(getNewCell(d.Criacao.ToString("dd/MM/yyyy"), font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                    table.AddCell(getNewCell(d.Valortotal.ToString(), font, Element.ALIGN_RIGHT, 10, PdfPCell.NO_BORDER));
                    totalprocesso = d.Valortotal + totalprocesso;

                }

                celltotal = getNewCell("Total Protocolo: " + totalprocesso.ToString(), titulo, Element.ALIGN_RIGHT, 10, PdfPCell.NO_BORDER);
                celltotal.Colspan = 7;
                table.AddCell(celltotal);

            }

            decimal total = totalprocesso + totalprotocolo;
            TotalGeral = getNewCell("Total Geral: " + total.ToString(), titulo, Element.ALIGN_RIGHT, 10, PdfPCell.NO_BORDER);
            TotalGeral.Colspan = 7;
            table.AddCell(TotalGeral);
            doc.Add(table);
        }
    }
}
