using PambolManager.API.Model.Dtos;
using PambolManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PambolManager.API.Model
{
    internal static class RoundExtensions
    {
        internal static RoundDto ToRoundDto(this Round round)
        {
            return new RoundDto
            {
                Id = round.Id,
                RoundNumber = round.RoundNumber,
                TournamentId = round.TournamentId
            };
        }
    }
}