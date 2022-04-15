using iText.Kernel.Events;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexttoSatReadingConverter.PdfService
{
    public class ImageEventHandler : IEventHandler
    {
        protected Image img;

        public ImageEventHandler(Image img)
        {
            this.img = img;
        }

        public void HandleEvent(Event @event)
        {
            throw new NotImplementedException();
        }
    }
}
