using PambolManager.API.Model.RequestModels;
using PambolManager.Domain.Entities;

namespace PambolManager.API.Model
{
    internal static class PlayerRequestModelsExtensions
    {
        internal static Player ToPlayer(this PlayerRequestModel requestModel)
        {

            return new Player
            {
                TeamId = requestModel.TeamId,
                Name = requestModel.Name,
                LastName = requestModel.LastName,
                Age = requestModel.Age,
                PhoneNumber = requestModel.PhoneNumber,
                Email = requestModel.Email
            };
        }

        internal static Player ToPlayer(this PlayerUpdateRequestModel requestModel, Player existingPlayer)
        {
            existingPlayer.Name = requestModel.Name;
            existingPlayer.LastName = requestModel.LastName;
            existingPlayer.Age = requestModel.Age;
            existingPlayer.PhoneNumber = requestModel.PhoneNumber;
            existingPlayer.Email = requestModel.Email;

            return existingPlayer;
        }
    }
}