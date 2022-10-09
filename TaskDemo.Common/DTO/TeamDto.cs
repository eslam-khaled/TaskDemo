using System;
using System.Collections.Generic;
using System.Text;

namespace DemoTask.DTO
{
    public class TeamDto:BaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PlayerDto> playerListDto { get; set; }
    }
}
