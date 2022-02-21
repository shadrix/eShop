namespace Catalog.Host.Models.Response;

public class GetTypesResponse<T>
{
    public IEnumerable<T> Data { get; set; } = null!;
}