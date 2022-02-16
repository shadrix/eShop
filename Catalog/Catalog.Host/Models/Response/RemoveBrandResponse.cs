namespace Catalog.Host.Models.Response;

public class RemoveBrandResponse<T>
{
    public T Id { get; set; } = default(T) !;
}