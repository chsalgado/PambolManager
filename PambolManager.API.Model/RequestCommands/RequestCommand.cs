using PambolManager.API.Model.Validation;

namespace PambolManager.API.Model.RequestCommands
{
    public class RequestCommand : IRequestCommand
    {
        [Minimum(1)]
        public int Page { get; set; }

        [Minimum(1)]
        [Maximum(50)]
        public int Take { get; set; }
    }
}
