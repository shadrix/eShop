#pragma warning disable CS8618
namespace Catalog.Host.Models.Dtos;

public class CatalogItemDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public string PictureUrl { get; set; }

    public Data.Entities.CatalogType CatalogType { get; set; }

    public Data.Entities.CatalogBrand CatalogBrand { get; set; }

    public int AvailableStock { get; set; }
}
