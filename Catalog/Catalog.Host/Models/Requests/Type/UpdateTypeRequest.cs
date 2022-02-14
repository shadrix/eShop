namespace Catalog.Host.Models.Requests.Type
{
    public class UpdateTypeRequest
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
    }
}
