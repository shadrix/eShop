using MVC.Models.Enums;

namespace MVC.Models.Dtos;

public class CatalogFilterDto
{
    public CatalogTypeFilter Type { get; set; }
    public int Value { get; set; }
}