using Microsoft.EntityFrameworkCore;
using Pedidos.Infra.Contexto;

namespace Pedidos.Api.Configuracao
{
    public static class ContextoConfiguracao
    {
        public static IServiceCollection AddContextoConfiguracao<TContext>(this IServiceCollection services, IConfiguration configuration) where TContext : BaseContexto
        {
            services.AddEntityFrameworkNpgsql()
                 .AddDbContext<TContext>(options =>
                 {
                     options.UseLoggerFactory(LoggerFactory.Create(build => build.AddConsole()));
                     options.UseNpgsql(configuration.GetConnectionString("BasePostgreSQL"));
                     options.EnableDetailedErrors().EnableSensitiveDataLogging();
                 }
                 );

            return services;
        }
   
        public static IApplicationBuilder ConfiguracaoEscopoNpgsql<TContext>(this IApplicationBuilder app) where TContext : BaseContexto
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<TContext>();
                dbContext.Database.Migrate();
            }
            return app;
        }
    }
}
