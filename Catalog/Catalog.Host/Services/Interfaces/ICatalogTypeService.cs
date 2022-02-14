namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogTypeService
    {
        Task<int> Create(string title);
        Task<int> Update(int id, string title);
        Task<int> Delete(int id);
    }
}
