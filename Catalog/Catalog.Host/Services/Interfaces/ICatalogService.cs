using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService
{
    Task<PaginatedItemsResponse<CatalogItemDto>?> GetCatalogItemsAsync(int pageSize, int pageIndex, Dictionary<CatalogTypeFilter, int>? filters);
    Task<GetByIdResponse<CatalogItemDto>> GetCatalogItemByIdAsync(int id);
    Task<GetBrandsResponse<CatalogItemDto>> GetCatalogItemsByBrandAsync(int id);
    Task<GetTypesResponse<CatalogItemDto>> GetCatalogItemsByTypeAsync(int id);
    Task<GetBrandsResponse<CatalogBrandDto>> GetCatalogBrandsAsync();
    Task<GetTypesResponse<CatalogTypeDto>> GetCatalogTypesAsync();
}