using PambolManager.API.Model.Dtos;
using PambolManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PambolManager.API.Model
{
    internal static class TeamExtensions
    {
        internal static TeamDto ToTeamDto(this Team team)
        {

            return new TeamDto
            {
                Id = team.Id,
                TeamName = team.TeamName,
                LogoPath = team.LogoPath,
                TournamentId = team.TournamentId
            };
        }
    }
}
