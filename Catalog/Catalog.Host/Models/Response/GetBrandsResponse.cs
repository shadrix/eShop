namespace Catalog.Host.Models.Response;

public class GetBrandsResponse<T>
{
    public IEnumerable<T> Data { get; set; } = null!;
}