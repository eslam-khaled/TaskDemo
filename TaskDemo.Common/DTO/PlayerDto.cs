using System;
using System.Collections.Generic;
using System.Text;

namespace DemoTask.DTO
{
    public class PlayerDto : BaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public byte[] Image { get; set; }
        public int CategoryId { get; set; }
    }
}
