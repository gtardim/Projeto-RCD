using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Reports
{
    public class ContasPagarReports : TNEReports
    {
        private int _tipodes;
        private DateTime? _data_i;
        private DateTime? _data_f;
        private bool _chkPagas;

        public ContasPagarReports(int tipodes, string data_i, string data_f,bool chkPagas)
        {
            Paisagem = false;
            _tipodes = tipodes;
            _chkPagas = chkPagas;

            if (data_i != null)
                _data_i = Convert.ToDateTime(data_i);
            else
                _data_i = null;

            if (data_f != null)
                _data_f = Convert.ToDateTime(data_f);
            else
                _data_f = null;

        }

        public int Tipodes { get => _tipodes; set => _tipodes = value; }
        public DateTime? Data_i { get => _data_i; set => _data_i = value; }
        public DateTime? Data_f { get => _data_f; set => _data_f = value; }
        public bool ChkPagas { get => _chkPagas; set => _chkPagas = value; }

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
            var contas = new List<Models.ContasPagar>();

            if (ChkPagas == true)
            {
                contas = new DAL.QuitarContasPagarDAL().ObterPagasRP(Tipodes, Data_i, Data_f);
            }
            else
            {
                contas = new DAL.QuitarContasPagarDAL().ObterAbertasRP(Tipodes, Data_i, Data_f);
            }

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
