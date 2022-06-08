using ExportToPdf.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ExportToPdf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var pdfFile = _reportService.GeneratePdfReport();
            return File(pdfFile, "application/octet-stream", "SimplePdf.pdf");
        }
    }
}
