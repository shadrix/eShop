namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogTypeService
    {
        Task AddAsync(string name);
        Task RemoveAsync(int id);
        Task UpdateAsync(int id, string name);
    }
}