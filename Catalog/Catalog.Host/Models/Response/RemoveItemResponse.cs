namespace Catalog.Host.Models.Response;

public class RemoveItemResponse<T>
{
    public T Id { get; set; } = default(T) !;
}