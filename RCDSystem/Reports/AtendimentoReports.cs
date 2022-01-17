using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Reports
{
    public class AtendimentoReports : TNEReports
    {
        private int _cliente;
        private DateTime? _data_i;
        private DateTime? _data_f;
        private int _protocolo;

        public AtendimentoReports(int cliente, string data_i, string data_f, int protocolo)
        {
            Paisagem = false;
            _cliente = cliente;
            _protocolo = protocolo;

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
        public int Protocolo { get => _protocolo; set => _protocolo = value; }

        public override void MontaCorpoDados()
        {
            base.MontaCorpoDados();


            #region Cabeçalho do Relatório
            PdfPTable table = new PdfPTable(5);
            BaseColor preto = new BaseColor(0, 0, 0);
            BaseColor fundo = new BaseColor(200, 200, 200);
            Font font = FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto);
            Font titulo = FontFactory.GetFont("Verdana", 8, Font.BOLD, preto);

            float[] colsW = { 10,16, 10,9,8};
            table.SetWidths(colsW);
            table.HeaderRows = 1;
            table.WidthPercentage = 100f;

            table.DefaultCell.Border = PdfPCell.BOTTOM_BORDER;
            table.DefaultCell.BorderColor = preto;
            table.DefaultCell.BorderColorBottom = new BaseColor(255, 255, 255);
            table.DefaultCell.Padding = 10;

            table.AddCell(getNewCell("Codigo", titulo, Element.ALIGN_LEFT, 10, PdfPCell.BOTTOM_BORDER, preto, fundo));
            table.AddCell(getNewCell("Titulo", titulo, Element.ALIGN_LEFT, 10, PdfPCell.BOTTOM_BORDER, preto, fundo));
            table.AddCell(getNewCell("Data", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOTTOM_BORDER, preto, fundo));
            table.AddCell(getNewCell("Hora Inicial", titulo, Element.ALIGN_RIGHT, 10, PdfPCell.BOTTOM_BORDER, preto, fundo));
            table.AddCell(getNewCell("Hora Final ", titulo, Element.ALIGN_RIGHT, 10, PdfPCell.BOTTOM_BORDER, preto, fundo));

            #endregion
            int cab = 0;

            var atend = new DAL.AtendimentoDAL().ObterAtendimentosRP(Cliente, Data_i, Data_f,Protocolo);
            if (atend != null)
            {

                foreach (var a in atend)
                {
                    if(cab == 0 || cab != a.Protocolo.Cod)
                    {
                        table.AddCell(getNewCell("Protocolo:" + a.Protocolo.Cod.ToString(), titulo, Element.ALIGN_LEFT, 14, PdfPCell.NO_BORDER, preto));
                        table.AddCell(getNewCell(a.Protocolo.Cliente.Nome, titulo, Element.ALIGN_LEFT, 14, PdfPCell.NO_BORDER, preto));
                        table.AddCell(getNewCell("", font));
                        table.AddCell(getNewCell("", font));
                        table.AddCell(getNewCell("", font));

                        table.AddCell("");
                        table.AddCell("");
                        table.AddCell("");
                        table.AddCell("");
                        table.AddCell("");

                        cab = a.Protocolo.Cod;

                    }

                    table.AddCell(getNewCell(a.Codigo.ToString(), font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                    table.AddCell(getNewCell(a.Titulo, font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                    table.AddCell(getNewCell(a.Data.ToString("dd/MM/yyyy"), font, Element.ALIGN_RIGHT, 10, PdfPCell.NO_BORDER));
                    table.AddCell(getNewCell(a.Horainicial.ToString("HH:MM"), font, Element.ALIGN_RIGHT, 10, PdfPCell.NO_BORDER));
                    table.AddCell(getNewCell(a.Horafinal.ToString("HH:MM"), font, Element.ALIGN_RIGHT, 10, PdfPCell.NO_BORDER));

                }
            }

            doc.Add(table);
        }
    }
}
