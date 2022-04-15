using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TexttoSatReadingConverter.Models;
using TexttoSatReadingConverter.PdfService;

namespace TexttoSatReadingConverter.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPdfGenerator _pdfGenerator;
        public HomeController(IPdfGenerator pdfGenerator)
        {
            _pdfGenerator = pdfGenerator;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GeneratePdf(string text)
        {
            var result = _pdfGenerator.GenerateDocument(text);
            return File(result, "application/pdf", "Document.pdf");

        }
    }
}
