namespace Catalog.Host.Models.Response;

public class GetByBrandResponse<T>
{
    public IEnumerable<T> Data { get; set; } = null!;
}