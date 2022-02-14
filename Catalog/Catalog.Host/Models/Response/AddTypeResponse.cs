namespace Catalog.Host.Models.Response
{
    public class AddTypeResponse<T>
    {
        public T Id { get; set; } = default(T) !;
    }
}
