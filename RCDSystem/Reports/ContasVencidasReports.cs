using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Reports
{
    public class ContasVencidasReports : TNEReports
    {

        public ContasVencidasReports()
        {
            Paisagem = false;

        }

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
            table.AddCell(getNewCell("Tipo da Despesa", titulo, Element.ALIGN_LEFT, 10, PdfPCell.BOTTOM_BORDER, preto, fundo));
            table.AddCell(getNewCell("Data de Vencimento", titulo, Element.ALIGN_LEFT, 10, PdfPCell.BOTTOM_BORDER, preto, fundo));
            table.AddCell(getNewCell("Valor", titulo, Element.ALIGN_RIGHT, 10, PdfPCell.BOTTOM_BORDER, preto, fundo));

            #endregion

            PdfPCell TotalGeral;
            decimal total = 0;

            var  contas = new DAL.QuitarContasPagarDAL().ObterVencidasRP();


            if (contas != null)
            {
                foreach (var d in contas)
                {
                    table.AddCell(getNewCell(d.Codigo.ToString(), font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                    table.AddCell(getNewCell(d.Descricao, font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                    table.AddCell(getNewCell(d.Tipodespesa.Descricao, font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                    table.AddCell(getNewCell(d.Datavenc.ToString("dd/MM/yyyy"), font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                    table.AddCell(getNewCell(d.Valor.ToString(), font, Element.ALIGN_RIGHT, 10, PdfPCell.NO_BORDER));
                    total = d.Valor + total;

                }

            }

            TotalGeral = getNewCell("Total Geral: " + total.ToString(), titulo, Element.ALIGN_RIGHT, 10, PdfPCell.NO_BORDER);
            TotalGeral.Colspan = 7;
            table.AddCell(TotalGeral);
            doc.Add(table);
        }
    }
}
