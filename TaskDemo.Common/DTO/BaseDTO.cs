using System;
using System.Collections.Generic;
using System.Text;

namespace DemoTask.DTO
{
    public class BaseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public bool ErrorCode { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
