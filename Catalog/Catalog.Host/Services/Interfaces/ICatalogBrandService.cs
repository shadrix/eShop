using Catalog.Host.Models.Requests;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogBrandService
{
    Task<int?> UpdateAsync(UpdateBrandRequest brand);
    Task<int?> AddAsync(CreateBrandRequest brand);
    Task<int?> RemoveAsync(int brandId);
}