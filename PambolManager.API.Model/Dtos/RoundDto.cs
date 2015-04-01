using System;

namespace PambolManager.API.Model.Dtos
{
    public class RoundDto : IDto
    {
        public Guid Id { get; set; }
        public Guid TournamentId { get; set; }
        
        public int RoundNumber { get; set; }
    }
}
