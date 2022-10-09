using System;
using System.Collections.Generic;
using System.Text;

namespace DemoTask.DTO
{
    public class LoginTokenDto :BaseDTO
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string Password { get; set; }
    }
}
