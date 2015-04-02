using System;
using System.ComponentModel.DataAnnotations;

namespace PambolManager.API.Model.RequestModels
{
    public abstract class TeamBaseRequestModel
    {
        [Required]
        public string TeamName { get; set; }
    }

    public class TeamRequestModel : TeamBaseRequestModel
    {
        public Guid TournamentId { get; set; }
    }

    public class TeamUpdateRequestModel : TeamBaseRequestModel
    {

    }
}
