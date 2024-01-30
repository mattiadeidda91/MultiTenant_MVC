namespace MultiTenant_MVC.DBContext.Factory
{
    public class TenantContextFactory : ITenantContextFactory
    {
        private TenantDbContext? _tenantDbContext;

        public TenantDbContext? GetTenant()
        {
            return _tenantDbContext;
        }

        public void SetTenant(TenantDbContext tenant)
        {
            _tenantDbContext = tenant;
        }

        public TenantDbContext GetTenantContext(string connectionString)
        {
            return new TenantDbContext(connectionString);
        }
    }
}
