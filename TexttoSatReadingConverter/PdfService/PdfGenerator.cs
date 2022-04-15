using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Jsoup.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TexttoSatReadingConverter.PdfService
{
    public class PdfGenerator : IPdfGenerator
    {
        public byte[] GenerateDocument( string text )
        {

            byte[] pdfFileBytes;
            using (var stream = new MemoryStream())
            using (var wri = new PdfWriter(stream))
            using (var pdf = new PdfDocument(wri))

            using (var doc = new Document(pdf))
            {
                string formattedText = Regex.Replace(text, @"\t|\n|\r", " ");
                string FONT_NINIONPRO = @".\resources\fonts\minionpro.ttf";
                PdfFont font = PdfFontFactory.CreateFont(FONT_NINIONPRO, PdfEncodings.IDENTITY_H);
                doc.SetFont(font);
                doc.SetRenderer(new CustomDocumentRenderer(doc));

                doc.Add(new Paragraph(text)
                    .SetMultipliedLeading(1.0f)
                    .SetFontSize(11));
                doc.Close();
                pdfFileBytes = stream.ToArray();

            }
            return pdfFileBytes;
        }
    }
}
