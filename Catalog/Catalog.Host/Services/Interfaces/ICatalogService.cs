using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService
{
    Task<DataListResponse<CatalogBrandDto>> GetBrandsAsync();
    Task<DataListResponse<CatalogItemDto>> GetByBrandAsync(string name);
    Task<DataResponse<CatalogItemDto>> GetByIdAsync(int id);
    Task<DataListResponse<CatalogItemDto>> GetByTypeAsync(string name);
    Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsAsync(int pageSize, int pageIndex);
    Task<DataListResponse<CatalogTypeDto>> GetTypesAsync();
}
