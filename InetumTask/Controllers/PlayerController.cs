using DemoTask.Business.BusinessInterface;
using DemoTask.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerBusiness _ProdcutBusiness;
        public PlayerController(IPlayerBusiness ProdcutBusiness)
        {
            _ProdcutBusiness = ProdcutBusiness;
        }


        [HttpGet]
        public IEnumerable<PlayerDto> GetPlayersListByTeamId(int TeamId)
        {
            return _ProdcutBusiness.GetPlayersListByTeamId(TeamId);
        }


        [HttpDelete]
        public bool DeletePlayerById(int Id)
        {
            return _ProdcutBusiness.DeletePlayerById(Id);
        }

    }
}
