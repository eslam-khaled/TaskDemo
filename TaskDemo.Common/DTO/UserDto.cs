using System;
using System.Collections.Generic;
using System.Text;

namespace DemoTask.DTO
{
    public class UserDto : BaseDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public string RefreshToken { get; set; }

    }
}
