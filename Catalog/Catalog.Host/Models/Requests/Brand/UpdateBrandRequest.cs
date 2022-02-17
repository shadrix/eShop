namespace Catalog.Host.Models.Requests.Brand
{
    public class UpdateBrandRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}