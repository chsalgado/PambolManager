using System;

namespace PambolManager.Domain.Entities.Core
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
