using System;

namespace PambolManager.API.Model.Dtos
{
    public class TeamDto : IDto
    {
        public Guid Id { get; set; }

        public string TeamName { get; set; }

        public string LogoPath { get; set; }

        public Guid TournamentId { get; set; }
    }
}
