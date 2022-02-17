namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogBrandService
    {
        Task AddAsync(string name);
        Task RemoveAsync(int id);
        Task UpdateAsync(int id, string name);
    }
}