namespace Catalog.Host.Models.Requests.Product
{
    public class UpdateProductRequest
    {
        public string OldName { get; set; } = null!;
        public string NewName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public string PictureFileName { get; set; } = null!;

        public int CatalogTypeId { get; set; }

        public int CatalogBrandId { get; set; }

        public int AvailableStock { get; set; }
    }
}