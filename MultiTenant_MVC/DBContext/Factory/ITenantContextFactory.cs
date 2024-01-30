namespace MultiTenant_MVC.DBContext.Factory
{
    public interface ITenantContextFactory
    {
        TenantDbContext GetTenantContext(string connectionString);
        TenantDbContext? GetTenant();
        void SetTenant(TenantDbContext tenant);
    }
}
