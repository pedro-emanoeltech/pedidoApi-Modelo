using Pedidos.App.AutoMapper;

namespace Pedidos.Api.Configuracao
{
    public static class AutoMapperConfiguracao
    {
        public static IServiceCollection AddAutoMapperConfiguracao(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperRequisicaoParaDomain));
            services.AddAutoMapper(typeof(MapperDomainParaResposta));
         
            return services;
        }
    }
}
