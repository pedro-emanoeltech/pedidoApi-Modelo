using FluentValidation;
using Pedidos.App.DTO.DtoRequisicao;
using Pedidos.App.Interface;
using Pedidos.App.Servico;
using Pedidos.Infra.Interface;
using Pedidos.Infra.Servico;
using static Pedidos.App.DTO.DtoRequisicao.ItemPedidoRequisicao;
using static Pedidos.App.DTO.DtoRequisicao.LoginRequisicao;
using static Pedidos.App.DTO.DtoRequisicao.PedidoRequisicao;
using static Pedidos.App.DTO.DtoRequisicao.UsuarioRequisicao;

namespace Pedidos.Api.Configuracao
{
    public static class InjecaoDependencias
    {
        public static IServiceCollection AdicionarInjecaoDependenciasConfiguracao(this IServiceCollection services)
        {
            //repositorio
            services.AddScoped<IPedidoRepositorio, PedidoRepositorio>();
            services.AddScoped<IUsuarioRepositorio,UsuarioRepositorio>();
             
            //serviços
            services.AddScoped<ITokenAcessoServico, TokenAcessoServico>();
            services.AddScoped<IPedidoServico, PedidoServico>();
            services.AddScoped<IAutenticacaoServico, AutenticacaoServico>();
            services.AddScoped<IUsuarioServico, UsuarioServico>();
        
 
            Validacoes(services);
            return services;
        }

        private static void Validacoes(IServiceCollection services)
        {
            //todo: utilizar reflexao para resolver as validações 
            services.AddTransient<IValidator<PedidoRequisicao>, PedidoRequisicaoValidator>();
            services.AddTransient<IValidator<ItemPedidoRequisicao>, ItemPedidoRequisicaoValidator>();
            services.AddTransient<IValidator<LoginRequisicao>, LoginRequisicaoValidator>();
            services.AddTransient<IValidator<UsuarioRequisicao>, UsuarioRequisicaoValidator>();
 
        }
    }
}
