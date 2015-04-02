using System;

namespace PambolManager.API.Model.Dtos
{
    public class TournamentDto : IDto
    {
        public Guid Id { get; set; }
        public string FieldManagerId { get; set; }
        
        public string TournamentName { get; set; }
        public int TotalRounds { get; set; }
        public int MaxTeams { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
    }
}
