namespace Catalog.Host.Models.Requests.Brand
{
    public class UpdateBrandRequest
    {
        public string OldName { get; set; } = null!;
        public string NewName { get; set; } = null!;
    }
}
