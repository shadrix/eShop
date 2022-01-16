using MVC.Dtos;
using MVC.Models.Dtos;

namespace MVC.Models.Requests;

public class CatalogPaginatedItemsRequest : PaginatedItemsRequest
{
    public List<CatalogFilterDto> Filters { get; set; }
}