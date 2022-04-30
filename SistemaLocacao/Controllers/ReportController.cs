using Microsoft.AspNetCore.Mvc;
using SistemaLocacao.Services.Report;

namespace SistemaLocacao.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("lateCostumers")]
        public async Task<ActionResult> ListLateCostumers([FromQuery] DateTime searchDate)
        {
            var response = await _reportService.ListLateCostumers(searchDate);
            return Ok(response);
        }

        [HttpGet("moviesNotLocated")]
        public async Task<ActionResult> ListMoviesNotLocated()
        {
            var response = await _reportService.ListMoviesNotLocated();
            return Ok(response);
        }
        
        [HttpGet("topMoviesLocated")]
        public async Task<ActionResult> ListTopMoviesLocated()
        {
            var response = await _reportService.ListTopMoviesLocated();
            return Ok(response);
        }

        [HttpGet("downMoviesLocated")]
        public async Task<ActionResult> ListDownMoviesLocated()
        {
            var response = await _reportService.ListDownMoviesLocated();
            return Ok(response);
        }

        [HttpGet("secondBestClient")]
        public async Task<ActionResult> ListSecondBestClient()
        {
            var response = await _reportService.ListSecondBestClient();
            return Ok(response);
        }

        [HttpGet("downloadData")]
        public async Task<ActionResult> DownloadData([FromQuery] DateTime searchLateCostumers)
        {
            try
            {
                await _reportService.DownloadReport(searchLateCostumers);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
