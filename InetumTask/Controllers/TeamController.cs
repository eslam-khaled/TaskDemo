using DemoTask.Business.BusinessInterface;
using DemoTask.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace DemoTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamBusiness _teamBusiness;
        public TeamController(ITeamBusiness teamBusiness)
        {
            _teamBusiness = teamBusiness;
        }

        [HttpPost]
        public TeamDto AddTeam(TeamDto teamDto)
        {
            return _teamBusiness.AddTeam(teamDto);
        }

        [HttpDelete]
        public bool DeleteTeamById(int Id)
        {
            return _teamBusiness.DeleteTeamById(Id);
        }

        [HttpPut]
        public TeamDto UpdateTeam(TeamDto teamDto)
        {
            return _teamBusiness.UpdateTeam(teamDto);
        }

        [HttpGet]
        public IEnumerable<TeamDto> GetTeamsList()
        {
            return _teamBusiness.GetTeamsList();
        }

        [HttpGet]
        [Route("GetTeamById")]
        public TeamDto GetTeamById(int Id)
        {
            return _teamBusiness.GetCategoryById(Id);
        }


    }
}
