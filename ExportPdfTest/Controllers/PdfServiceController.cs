using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace ExportPdfTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfServiceController : ControllerBase
    {
        private readonly IGeneratePdf _generatePdf;

        public PdfServiceController(IGeneratePdf generatePdf)
        {
            _generatePdf = generatePdf;
        }

        [HttpGet]
        public IActionResult ExportToPdf()
        {
            //var formattedHtml = $@"
            //<!DOCTYPE html>
            //<html lang=""en"">
            //<head>
            //    This is the header of this document.
            //</head>
            //<body>
            //<h1>This is the heading for demonstration purposes only.</h1>
            //<p>This is a line of text for demonstration purposes only.</p>
            //</body>
            //</html>
            //";

            var formattedHtml = System.IO.File.ReadAllText("Views/Index.cshtml");

            var pdf = _generatePdf.GetPDF(formattedHtml);
            var stream = new MemoryStream(pdf);
            var fileName = "SamplePdf.pdf";

            return File(stream, "application/octet-stream", fileName);
        }
    }
}
