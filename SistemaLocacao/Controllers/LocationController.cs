using Microsoft.AspNetCore.Mvc;
using SistemaLocacao.Models;
using SistemaLocacao.Services.Location;

namespace SistemaLocacao.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            if (id > 0)
            {
                var location = await _locationService.FindById(id);
                if (location != null)
                {
                    return Ok(location);
                }
                return NotFound();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> FindAll()
        {
            var response = await _locationService.FindAll();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Location>> Create([FromBody] Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dados invalidos!");
            }

            try
            {
                var response = await _locationService.Create(location);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException + " " + ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Location>> Update([FromBody] Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dados invalidos!");
            }

            try
            {
                var response = await _locationService.Update(location);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException + " " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id > 0)
            {
                var isDeleted = await _locationService.Delete(id);
                if (isDeleted)
                {
                    return NoContent();
                }
                return NotFound();
            }
            return BadRequest();
        }

    }
}
