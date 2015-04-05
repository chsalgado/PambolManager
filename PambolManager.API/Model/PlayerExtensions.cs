using PambolManager.API.Model.Dtos;
using PambolManager.Domain.Entities;

namespace PambolManager.API.Model
{
    internal static class PlayerExtensions
    {
        internal static PlayerDto ToPlayerDto(this Player player)
        {
            return new PlayerDto
            {
                Id = player.Id,
                Name = player.Name,
                LastName = player.LastName,
                Age = player.Age,
                PhoneNumber = player.PhoneNumber,
                Email = player.Email,
                TeamId = player.TeamId
            };
        }
    }
}
