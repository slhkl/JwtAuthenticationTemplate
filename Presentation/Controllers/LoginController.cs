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
            var user = new UserDto();

            var client = _clientService.Get(c => c.Email.Equals(loginDto.Email) && c.Password.Equals(loginDto.Password));

            if (client != null)
            {
                user.Id = client.Id;
                user.Name = client.Name;
                user.Role = "Client";
                user.Email = client.Email;

                return Ok(TokenProvider.Instance.CreateToken(user));
            }

            var customer = _customerService.Get(c => c.Email.Equals(loginDto.Email) && c.Password.Equals(loginDto.Password));

            if (customer != null)
            {
                user.Id = customer.Id;
                user.Name = customer.Name;
                user.Role = "Customer";
                user.Email = customer.Email;

                return Ok(TokenProvider.Instance.CreateToken(user));
            }

            return Unauthorized();
        }
    }
}
