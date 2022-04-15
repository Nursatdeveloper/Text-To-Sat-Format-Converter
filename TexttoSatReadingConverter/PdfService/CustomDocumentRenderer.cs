using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
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
            new Rectangle(46, 36, 234, 750),
            new Rectangle(320, 36, 234, 750)
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
                new PdfCanvas(document.GetPdfDocument(), document.GetPdfDocument().GetNumberOfPages())
                    .MoveTo(297.5f, 36)
                    .LineTo(297.5f, 806)
                    .SetLineDash(1.2f, 1.2f, 1.0f)
                    .Stroke();
                    
            }

            currentArea = new RootLayoutArea(currentPageNumber, COLUMNS[nextAreaNumber % 2].Clone());
            nextAreaNumber++;
            return currentArea;
        }
    }
}
