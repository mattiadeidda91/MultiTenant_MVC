namespace MultiTenant_MVC.Models.Entities
{
    public class Tenant
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Host { get; set; }
        public string? DatabaseConnectionString { get; set; }

        public virtual MasterUser? User { get; set; }
    }
}
