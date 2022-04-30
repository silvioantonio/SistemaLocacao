using Microsoft.AspNetCore.Mvc;
using SistemaLocacao.Services.Movie;

namespace SistemaLocacao.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost("UploadFile")]
        public async Task<ActionResult> UploadData(IFormFile file)
        {
            try
            {
                await _movieService.Upload(file);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }
        
    }
}
