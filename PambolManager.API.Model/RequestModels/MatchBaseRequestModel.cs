using System;
using System.ComponentModel.DataAnnotations;

namespace PambolManager.API.Model.RequestModels
{
    public abstract class MatchBaseRequestModel
    {
        [Required]
        public int HomeGoals { get; set; }

        [Required]
        public int AwayGoals { get; set; }

        [Required]
        public bool IsScoreSet { get; set; }
    }

    public class MatchRequestModel : MatchBaseRequestModel
    {
        [Required]
        public Guid RoundId { get; set; }

        [Required]
        public Guid HomeTeamId { get; set; }

        [Required]
        public Guid AwayTeamId { get; set; }
    }

    public class MatchUpdateRequestModel : MatchBaseRequestModel
    {

    }
}