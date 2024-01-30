using Microsoft.EntityFrameworkCore;
using MultiTenant_MVC.DBContext;
using MultiTenant_MVC.DBContext.Factory;

namespace MultiTenant_MVC.Middlewares
{
    public class MultiTenantMiddleware : IMiddleware
    {
        private readonly ITenantContextFactory _tenantContextFactory;
        private readonly MasterDbContext _masterDbContext;

        public MultiTenantMiddleware(ITenantContextFactory tenantContextFactory, MasterDbContext masterDbContext)
        {
            _tenantContextFactory = tenantContextFactory;
            _masterDbContext = masterDbContext;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            ArgumentNullException.ThrowIfNull(context);

            // Effettua l'autenticazione dell'utente e recupera la connessione al database dedicato
            var name = context?.User?.Identity?.Name;

            if (name != null && !context!.Items.ContainsKey("DedicatedDbContext") && _tenantContextFactory.GetTenant() == null)
            {
                var user = await _masterDbContext.Users.SingleOrDefaultAsync(u => u.Name == name);

                if (user != null)
                {
                    var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();
                    optionsBuilder.UseSqlServer(user.ConnectionString);
                    var dedicatedDbContext = new TenantDbContext(optionsBuilder.Options);

                    // Registra il DbContext dedicato per l'utente corrente
                    //context!.Items["DedicatedDbContext"] = dedicatedDbContext;

                    _tenantContextFactory.SetTenant(dedicatedDbContext);
                }
            }

            await next(context!);
        }
    }

}
