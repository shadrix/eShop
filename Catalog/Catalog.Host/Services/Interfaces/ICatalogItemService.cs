namespace Catalog.Host.Services.Interfaces;

public interface ICatalogItemService
{
    Task<int?> Create(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<bool> Delete(string name);
    Task<bool> Update(string oldName, string newName, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
}