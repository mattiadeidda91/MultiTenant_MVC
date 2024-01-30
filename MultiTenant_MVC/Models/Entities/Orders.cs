namespace MultiTenant_MVC.Models.Entities
{
    public class Orders
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public string? Price { get; set; }
    }
}
