namespace MultiTenant_MVC.Models.Entities
{
    public class MasterUser
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConnectionString { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}
