using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService
{
    Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsAsync(int pageSize, int pageIndex);
    Task<CatalogItemDto> GetByIdAsync(int id);
    Task<IEnumerable<CatalogItemDto>> GetByBrandAsync(string brandTitle);
    Task<IEnumerable<CatalogItemDto>> GetByTypeAsync(string typeTitle);
    Task<IEnumerable<CatalogBrandDto>> GetBrandsAsync();
    Task<IEnumerable<CatalogTypeDto>> GetTypesAsync();
}