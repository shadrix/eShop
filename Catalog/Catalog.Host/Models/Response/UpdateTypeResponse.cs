namespace Catalog.Host.Models.Response;

public class UpdateTypeResponse<T>
{
    public T Result { get; set; } = default(T) !;
}