namespace Catalog.Host.Models.Response;

public class GetByIdResponse<T>
{
    public T Item { get; set; } = default(T) !;
}