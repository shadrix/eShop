namespace MVC.Dtos;

public class GetTypeRequest<T>
{
    public IEnumerable<T> Types { get; set; } = null!;
}