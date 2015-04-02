using System;

namespace PambolManager.API.Model.Dtos
{
    public class PlayerDto : IDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
