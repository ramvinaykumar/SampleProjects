using Microsoft.AspNetCore.Mvc;
using PdfExport.Repository;

namespace PdfExport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IPdfGeneration _pdfGeneration;
        public ReportController(IPdfGeneration pdfGeneration)
        {
            _pdfGeneration = pdfGeneration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var pdfFile = _pdfGeneration.GeneratePdfReport();
            return File(pdfFile, "application/octet-stream", "SimplePdf.pdf");
        }
    }
}
