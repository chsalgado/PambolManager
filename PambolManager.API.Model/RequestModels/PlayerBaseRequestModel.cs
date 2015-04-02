using System;
using System.ComponentModel.DataAnnotations;

namespace PambolManager.API.Model.RequestModels
{
    public abstract class PlayerBaseRequestModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string LastName { get; set; }

        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

    }

    public class PlayerRequestModel : PlayerBaseRequestModel
    {
        public Guid TeamId { get; set; }
    }

    public class PlayerUpdateRequestModel : PlayerBaseRequestModel
    {

    }
}