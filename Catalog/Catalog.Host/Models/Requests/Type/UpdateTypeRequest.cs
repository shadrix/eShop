namespace Catalog.Host.Models.Requests.Type
{
    public class UpdateTypeRequest
    {
        public string OldName { get; set; } = null!;
        public string NewName { get; set; } = null!;
    }
}
