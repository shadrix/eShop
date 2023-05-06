namespace Catalog.Host.Models.Response;

public class UpdateItemResponse<T>
{
    public T Result { get; set; } = default(T) !;
}