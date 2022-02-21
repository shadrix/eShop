namespace Catalog.Host.Models.Response;

public class UpdateBrandResponse<T>
{
    public T Result { get; set; } = default(T) !;
}