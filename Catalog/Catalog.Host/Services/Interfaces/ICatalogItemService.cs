using Catalog.Host.Models.Requests;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogItemService
{
    Task<int?> AddAsync(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<int?> UpdateAsync(UpdateProductRequest product);
    Task<int?> RemoveAsync(int itemId);
}