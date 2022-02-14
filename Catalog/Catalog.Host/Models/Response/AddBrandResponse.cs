namespace Catalog.Host.Models.Response
{
    public class AddBrandResponse<T>
    {
        public T Id { get; set; } = default(T) !;
    }
}
