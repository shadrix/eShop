namespace Catalog.Host.Models.Requests
{
    public class UpdateProductRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public string? PictureFileName { get; set; }

        public int? CatalogTypeId { get; set; }

        public int? CatalogBrandId { get; set; }

        public int? AvailableStock { get; set; }
    }
}
