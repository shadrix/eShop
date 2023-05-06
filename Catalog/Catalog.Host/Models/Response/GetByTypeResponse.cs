namespace Catalog.Host.Models.Response;

public class GetByTypeResponse<T>
{
    public IEnumerable<T> Data { get; set; } = null!;
}