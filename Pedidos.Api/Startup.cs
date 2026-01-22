using Pedidos.Api.Configuracao;
using Pedidos.Infra.Contexto;

namespace Pedidos.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddContextoConfiguracao<PedidosContexto>(Configuration);
            services.AddApiConfiguracao(Configuration);
            services.AdicionarInjecaoDependenciasConfiguracao();
            services.AddControllers();
            services.AddAutoMapperConfiguracao();
            services.AddHostedService<CriaUsuarioIncialServico>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.ConfiguracaoEscopoNpgsql<PedidosContexto>();
            app.UseApiConfiguracao();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
