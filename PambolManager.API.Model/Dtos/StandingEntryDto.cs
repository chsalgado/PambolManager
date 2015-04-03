namespace PambolManager.API.Model.Dtos
{
    public class StandingEntryDto
    {
        public string TeamName { get; set; }
        public int PlayedMatches { get; set; }
        public int WonMatches { get; set; }
        public int DrawedMatches { get; set; }
        public int LostMatches { get; set; }
        public int ScoredGoals { get; set; }
        public int ReceivedGoals { get; set; }
        public int GoalsDifference { get; set; }
        public int Points { get; set; }
    }
}
