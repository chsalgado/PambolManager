using System;

namespace PambolManager.Domain.Entities.Core
{
    public interface IEntity
    {
        Guid Key { get; set; }
    }
}
