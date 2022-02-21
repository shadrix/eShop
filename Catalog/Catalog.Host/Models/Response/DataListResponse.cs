namespace Catalog.Host.Models.Response
{
    public class DataListResponse<T>
    {
        public IEnumerable<T> Data { get; set; } = null!;
    }
}
