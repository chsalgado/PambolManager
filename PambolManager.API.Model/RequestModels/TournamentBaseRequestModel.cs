using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PambolManager.API.Model.RequestModels
{
    public abstract class TournamentBaseRequestModel
    {
        [Required]
        public string TournamentName { get; set; }

        [Required]
        public int MaxTeams { get; set; }

        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
    }

    public class TournamentRequestModel :TournamentBaseRequestModel
    {
        public string FieldManagerId { get; set; }
    }

    public class TournamentUpdateRequestModel : TournamentBaseRequestModel
    {

    }
}
