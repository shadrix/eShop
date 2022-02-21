namespace Catalog.Host.Models.Response;

public class RemoveTypeResponse<T>
{
    public T Id { get; set; } = default(T) !;
}