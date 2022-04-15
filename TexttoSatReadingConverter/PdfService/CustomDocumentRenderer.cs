using iText.IO.Image;
using iText.Kernel.Geom;
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
            new Rectangle(46, 46, 234, 710),
            new Rectangle(320, 46, 234, 710)
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


                new PdfCanvas(document.GetPdfDocument(), document.GetPdfDocument().GetNumberOfPages())
                    .MoveTo(297.5f, 36)
                    .LineTo(297.5f, 750)
                    .SetLineDash(1.2f, 1.2f, 1.0f)
                    .Stroke()
                    .AddImageAt(img, 440, 20, true);

                new PdfCanvas(document.GetPdfDocument(), document.GetPdfDocument().GetNumberOfPages())
                    .AddImageAt(header, 45, 770, true);
                    
                    
            }

            currentArea = new RootLayoutArea(currentPageNumber, COLUMNS[nextAreaNumber % 2].Clone());
            nextAreaNumber++;
            return currentArea;
        }
    }
}
