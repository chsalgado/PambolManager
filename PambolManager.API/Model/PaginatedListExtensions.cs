using PambolManager.API.Model.Dtos;
using PambolManager.Domain.Entities.Core;
using System.Collections.Generic;

namespace PambolManager.API.Model
{
    internal static class PaginatedListExtensions
    {
        internal static PaginatedDto<TDto> ToPaginatedDto<TDto, TEntity>(
            this PaginatedList<TEntity> source,
            IEnumerable<TDto> items) where TDto : IDto
        {
            return new PaginatedDto<TDto>
            {
                PageIndex = source.PageIndex,
                PageSize = source.PageSize,
                TotalCount = source.TotalCount,
                TotalPageCount = source.TotalPageCount,
                HasNextPage = source.HasNextPage,
                HasPreviousPage = source.HasPreviousPage,
                Items = items
            };
        }

    }
}
