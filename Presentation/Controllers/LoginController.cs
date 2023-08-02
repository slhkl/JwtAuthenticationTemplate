using Business.Discrete;
using Data.Dto;
using Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Authorization;

namespace Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IService<Client> _clientService;

        private IService<Customer> _customerService;

        public LoginController(IService<Client> clientService, IService<Customer> customerService)
        {
            _clientService = clientService;
            _customerService = customerService;
        }

        [HttpPost]
        public IActionResult Login(LoginDto loginDto)
        {
            var client = _clientService.Get(c => c.Email.Equals(loginDto.Email) && c.Password.Equals(loginDto.Password));

            if (client != null)
            {
                loginDto.Id = client.Id;
                loginDto.Name = client.Name;
                loginDto.Role = "Client";

                return Ok(TokenProvider.Instance.CreateToken(loginDto));
            }

            var customer = _customerService.Get(c => c.Email.Equals(loginDto.Email) && c.Password.Equals(loginDto.Password));

            if (customer != null)
            {
                loginDto.Id = customer.Id;
                loginDto.Name = customer.Name;
                loginDto.Role = "Customer";

                return Ok(TokenProvider.Instance.CreateToken(loginDto));
            }

            return Unauthorized();
        }
    }
}
