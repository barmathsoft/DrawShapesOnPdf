using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShapesOnPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            XPoint location = new XPoint(200,200);
            XSize size = new XSize(20, 50);

            PdfSharp.Pdf.PdfDocument seperateDocument = new PdfSharp.Pdf.PdfDocument();
            PdfSharp.Pdf.PdfDocument modifyDocument = new PdfSharp.Pdf.PdfDocument();
            seperateDocument = PdfSharp.Pdf.IO.PdfReader.Open("selectedPath", PdfSharp.Pdf.IO.PdfDocumentOpenMode.Import);


            var seperateDocPages = seperateDocument.PageCount;

            for (int i = 0; i < seperateDocPages; i++)
            {
                PdfSharp.Pdf.PdfDocument outputDocument = new PdfSharp.Pdf.PdfDocument();
                outputDocument.AddPage(seperateDocument.Pages[i]);
                outputDocument.Save(Path.Combine("savedPath", i + ".pdf"));
            }

            string[] seperatedFiles = Directory.GetFiles("savedPath");

            var modifyDocPages = modifyDocument.PageCount;

            for (int i = 0; i < seperatedFiles.Length; i++)
            {
                modifyDocument = PdfSharp.Pdf.IO.PdfReader.Open(seperatedFiles[i], PdfSharp.Pdf.IO.PdfDocumentOpenMode.Modify);
                PdfSharp.Pdf.PdfPage eachPage = modifyDocument.Pages[0];
                PdfSharp.Drawing.XGraphics pdfGraphic = PdfSharp.Drawing.XGraphics.FromPdfPage(eachPage);
                pdfGraphic.DrawRectangle(XPens.DarkBlue, new XRect(location, size));
                modifyDocument.Save(Path.Combine("savedPath", "__" + i + ".pdf"));
            }
        }
    }
}
