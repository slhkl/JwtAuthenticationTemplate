using Business.Discrete;
using Data.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "CustomAccessPolicy", Roles = "Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IService<Customer> _service;

        public CustomerController(IService<Customer> service)
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
        public IActionResult Post([FromBody] Customer customer)
        {
            _service.Add(customer);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody] Customer customer)
        {
            _service.Update(customer);
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
