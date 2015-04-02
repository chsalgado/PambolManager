using PambolManager.API.Model.Dtos;
using PambolManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            };
        }
    }
}
