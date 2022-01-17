using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Reports
{
    public class ProtocoloReports : TNEReports
    {
        private int _cliente;
        private int _status;
        private DateTime? _data_i;
        private DateTime? _data_f;

        public ProtocoloReports(int cliente, int status, string data_i, string data_f)
        {
            Paisagem = false;
            _cliente = cliente;
            _status = status;

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
        public int Status { get => _status; set => _status = value; }
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
            table.AddCell(getNewCell("Status", titulo, Element.ALIGN_LEFT, 10, PdfPCell.BOTTOM_BORDER, preto, fundo));
            table.AddCell(getNewCell("Data Criação", titulo, Element.ALIGN_RIGHT, 10, PdfPCell.BOTTOM_BORDER, preto, fundo));

            #endregion


            var prot = new DAL.ProtocoloDAL().ObterProtocolosRP(Cliente, Status, Data_i, Data_f);
            if (prot != null)
            {
                foreach (var p in prot)
                {
                    table.AddCell(getNewCell(p.Cod.ToString(), font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                    table.AddCell(getNewCell(p.Descricao, font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                    table.AddCell(getNewCell(p.Cliente.Nome, font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                    table.AddCell(getNewCell(p.Status.Descricao, font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                    table.AddCell(getNewCell(p.Criacao.ToString("dd/MM/yyyy"), font, Element.ALIGN_RIGHT, 10, PdfPCell.NO_BORDER));

                }

            }

            doc.Add(table);
        }
    }
}
