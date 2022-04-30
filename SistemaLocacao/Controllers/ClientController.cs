using Microsoft.AspNetCore.Mvc;
using SistemaLocacao.Models;
using SistemaLocacao.Services.Client;

namespace SistemaLocacao.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            if (id > 0)
            {
                var client = await _clientService.FindById(id);
                if (client == null)
                {
                    return NotFound();
                }
                return Ok(client);
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> FindAll()
        {
            var response = await _clientService.FindAll();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Client>> Create([FromBody] Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dados invalidos!");
            }

            try
            {
                var response = await _clientService.Create(client);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Cliente com esse nome e cpf ja cadastrados");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Client>> Update([FromBody] Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dados invalidos!");
            }

            try
            {
                var response = await _clientService.Update(client);
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
                var isDeleted = await _clientService.Delete(id);
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
