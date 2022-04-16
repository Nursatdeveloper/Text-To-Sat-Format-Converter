using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Layout;
using iText.Layout.Renderer;

namespace TexttoSatReadingConverter.PdfService
{
    public class CustomDocumentRenderer : DocumentRenderer
    {
        int nextAreaNumber = 0;
        int currentPageNumber;

        public static readonly Rectangle[] COLUMNS =
        {
            new Rectangle(46, 86, 234, 650),
            new Rectangle(320, 86, 234, 650)
        };

        public CustomDocumentRenderer(Document document) : base(document)
        {
        }


        public override IRenderer GetNextRenderer()
        {
            return new DocumentRenderer(document);
        }

        protected override LayoutArea UpdateCurrentArea(LayoutResult overflowResult)
        {
            if (nextAreaNumber % 2 == 0)
            {
                currentPageNumber = base.UpdateCurrentArea(overflowResult).GetPageNumber();
            }
            else
            {
                string imgPath = @".\resources\images\continue.png";
                string headerPath = @".\resources\images\header.png";
                ImageData img = ImageDataFactory.Create(imgPath);
                ImageData header = ImageDataFactory.Create(headerPath);

                PdfPage page = document.GetPdfDocument().GetPage(currentPageNumber);
                int pageNumber = document.GetPdfDocument().GetPageNumber(page);

                string FONT_NINIONPRO_BOLD = @".\resources\fonts\minionprobold.ttf";
                PdfFont font = PdfFontFactory.CreateFont(FONT_NINIONPRO_BOLD, PdfEncodings.IDENTITY_H);

                new PdfCanvas(document.GetPdfDocument(), document.GetPdfDocument().GetNumberOfPages())
                    .MoveTo(297.5f, 76)
                    .LineTo(297.5f, 740)
                    .SetLineDash(1.2f, 1.2f, 1.0f)
                    .Stroke()
                    .AddImageAt(img, 440, 20, true);

                new PdfCanvas(document.GetPdfDocument(), document.GetPdfDocument().GetNumberOfPages())
                    .BeginText()
                    .MoveText(294.5f, 30)
                    .SetFontAndSize(font, 13)
                    .ShowText($"{pageNumber}")
                    .EndText()
                    .AddImageAt(header, 45, 770, true);

                    
                    
            }

            currentArea = new RootLayoutArea(currentPageNumber, COLUMNS[nextAreaNumber % 2].Clone());
            nextAreaNumber++;
            return currentArea;
        }
    }
}
