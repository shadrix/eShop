namespace Catalog.Host.Services.Interfaces;

public interface ICatalogItemService
{
    Task<int> CreateProductAsync(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
}