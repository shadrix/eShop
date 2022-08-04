namespace MVC.Dtos;

public class GetBrandRequest<T>
{
    public IEnumerable<T> Brands { get; set; } = null!;
}