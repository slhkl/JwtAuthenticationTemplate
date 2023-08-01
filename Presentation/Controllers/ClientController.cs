using Business.Discrete;
using Data.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "CustomAccessPolicy", Roles = "Client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IService<Client> _service;

        public ClientController(IService<Client> service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_service.Get(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Client client)
        {
            _service.Add(client);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody] Client client)
        {
            _service.Update(client);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}
