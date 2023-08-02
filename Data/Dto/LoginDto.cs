
using System.Text.Json.Serialization;

namespace Data.Dto
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public string? Name { get; set; }
        [JsonIgnore]
        public string? Role { get; set; }
    }
}
