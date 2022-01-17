using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Reports
{
    public class ReciboReports : TNEReports
    {

        private int _codigo;

        public ReciboReports(int codigo)
        {
            Paisagem = false;
            _codigo = codigo;

        }

        public int Codigo { get => _codigo; set => _codigo = value; }

        public override void MontaCorpoDados()
        {
            base.MontaCorpoDados();


            #region Cabeçalho do Relatório
            PdfPTable table = new PdfPTable(1);
            BaseColor preto = new BaseColor(0, 0, 0);
            BaseColor fundo = new BaseColor(200, 200, 200);
            Font font = FontFactory.GetFont("Verdana", 16, Font.NORMAL, preto);
            Font assinatura = FontFactory.GetFont("Verdana", 16, Font.BOLD, preto);

            float[] colsW = { 100 };
            table.SetWidths(colsW);
            table.HeaderRows = 1;
            table.WidthPercentage = 100f;

            table.DefaultCell.Border = PdfPCell.BOTTOM_BORDER;
            table.DefaultCell.BorderColor = preto;
            table.DefaultCell.BorderColorBottom = new BaseColor(255, 255, 255);
            table.DefaultCell.Padding = 10;


            #endregion


            var recibo = new DAL.QuitarContasReceberDAL().ObterPaga(Codigo);
            if (recibo != null)
            {

                table.AddCell(getNewCell(" ", font));
                table.AddCell(getNewCell(" ", font));
                table.AddCell(getNewCell(" ", font));
                table.AddCell(getNewCell(" ", font));
                table.AddCell(getNewCell(" ", font));
                table.AddCell(getNewCell(" ", font));
                table.AddCell(getNewCell(" ", font));
                table.AddCell(getNewCell(" ", font));

                table.AddCell(getNewCell("Declado de Recebi do Cliente " + recibo.Cliente.Nome + " o Valor R$ " + recibo.Valorpago.ToString() + " referente ao pagamento do " + recibo.Descricao + " na data de " + recibo.Pago?.ToString("dd/MM/yyyy"), font, Element.ALIGN_CENTER, 10, PdfPCell.NO_BORDER));
                table.AddCell(getNewCell(" ", font));
                table.AddCell(getNewCell(" ", font));
                table.AddCell(getNewCell(" ", font));

                table.AddCell(getNewCell("________________________________________", assinatura, Element.ALIGN_CENTER, 10, PdfPCell.NO_BORDER));
                table.AddCell(getNewCell(recibo.Cliente.Nome, assinatura, Element.ALIGN_CENTER, 10, PdfPCell.NO_BORDER));

            }

            doc.Add(table);
        }
    }
}
