using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Reports
{
    public class ArquivoReports : TNEReports
    {
        private int _cliente;
        private int _tipoarq;
        private int _processo;


        public ArquivoReports(int cliente, int tipoarq,int processo)
        {
            Paisagem = false;
            _cliente = cliente;
            _tipoarq = tipoarq;
            _processo = processo;

        }

        public int Cliente { get => _cliente; set => _cliente = value; }
        public int Tipoarq { get => _tipoarq; set => _tipoarq = value; }
        public int Processo { get => _processo; set => _processo = value; }

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
            table.AddCell(getNewCell("Tipo", titulo, Element.ALIGN_LEFT, 10, PdfPCell.BOTTOM_BORDER, preto, fundo));
            table.AddCell(getNewCell("Data Inclusão", titulo, Element.ALIGN_RIGHT, 10, PdfPCell.BOTTOM_BORDER, preto, fundo));

            #endregion


            var arq = new DAL.RegistrarArquivoDAL().ObterArquivosRP(Cliente, Tipoarq,Processo);
            if (arq != null)
            {
                if(Processo != 0)
                {
                    table.AddCell(getNewCell("Processo:"+Processo.ToString(), titulo, Element.ALIGN_LEFT, 14, PdfPCell.NO_BORDER, preto));
                    table.AddCell(getNewCell("", font));
                    table.AddCell(getNewCell("", font));
                    table.AddCell(getNewCell("", font));
                    table.AddCell(getNewCell("", font));

                    table.AddCell("");
                    table.AddCell("");
                    table.AddCell("");
                    table.AddCell("");
                    table.AddCell("");

                    foreach (var a in arq)
                    {
                        table.AddCell(getNewCell(a.Cod.ToString(), font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                        table.AddCell(getNewCell(a.Descricao, font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                        table.AddCell(getNewCell(a.Cli.Nome, font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                        table.AddCell(getNewCell(a.Tipoarq.Descricao, font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                        table.AddCell(getNewCell(a.DataInclusao.ToString("dd/MM/yyyy"), font, Element.ALIGN_RIGHT, 10, PdfPCell.NO_BORDER));

                    }
                }
                else
                {
                    foreach (var a in arq)
                    {
                        table.AddCell(getNewCell(a.Cod.ToString(), font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                        table.AddCell(getNewCell(a.Descricao, font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                        table.AddCell(getNewCell(a.Cli.Nome, font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                        table.AddCell(getNewCell(a.Tipoarq.Descricao, font, Element.ALIGN_LEFT, 10, PdfPCell.NO_BORDER));
                        table.AddCell(getNewCell(a.DataInclusao.ToString("dd/MM/yyyy"), font, Element.ALIGN_RIGHT, 10, PdfPCell.NO_BORDER));

                    }
                }


            }

            doc.Add(table);
        }
    }
}
