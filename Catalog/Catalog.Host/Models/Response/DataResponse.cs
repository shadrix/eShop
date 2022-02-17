namespace Catalog.Host.Models.Response
{
    public class DataResponse<T>
    {
        public T Data { get; set; } = default(T) !;
    }
}
