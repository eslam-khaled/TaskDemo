using DemoTask.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoTask.Business.BusinessInterface
{
    public interface ITeamBusiness
    {
        /// <summary>
        /// Get all teams
        /// </summary>
        /// <returns><see cref=""/></returns>
        ICollection<TeamDto> GetTeamsList();

        /// <summary>
        /// Get one team by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns><see cref="Id"/></returns>
        TeamDto GetCategoryById(int Id);

        /// <summary>
        /// Add new team
        /// </summary>
        /// <param name="teamDto"><see cref="teamDto"/></param>
        /// <returns></returns>
        TeamDto AddTeam(TeamDto teamDto);

        /// <summary>
        /// Update team info
        /// </summary>
        /// <param name="teamDto"><see cref="teamDto"/></param>
        /// <returns></returns>
        TeamDto UpdateTeam(TeamDto teamDto);

        /// <summary>
        /// Delete team by id
        /// </summary>
        /// <param name="Id"><see cref="Id"/></param>
        /// <returns></returns>
        bool DeleteTeamById(int Id);

    }
}
