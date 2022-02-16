namespace Catalog.Host.Services.Interfaces;

public interface ICatalogItemService
{
    Task<int?> Create(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<bool> Delete(int id);
    Task<bool> Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
}