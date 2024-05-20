using Business.Concrete.Model;
using Data.Dto;
using System.Security.Claims;

namespace Presentation.Configuration
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, RequestMiddlewareModel model)
        {
            model.User = new UserDto
            {
                Email = httpContext.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email))?.Value ?? string.Empty,
                Id = int.Parse(httpContext.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value ?? "0"),
                Name = httpContext.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Name))?.Value ?? string.Empty,
                Role = httpContext.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role))?.Value ?? string.Empty
            };

            await _next(httpContext);
        }
    }
}
