using Pedidos.App.DTO.DtoRequisicao;
using Pedidos.App.Interface;

namespace Pedidos.Api.Configuracao
{
    //deletar essa classe caso não queira criar um usuario admin inicial
    public class CriaUsuarioIncialServico : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public CriaUsuarioIncialServico(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var usuarioServico = scope.ServiceProvider.GetRequiredService<IUsuarioServico>();


            try
            {
                var admin = new UsuarioRequisicao
                {
                    Nome = "Administrador",
                    Email = "admin@exemplo.com",
                    Senha = "Admin@123",
                };

                await usuarioServico.AdicionarAsync(admin);
            }
            catch
            {
                // Se o email já existir, não faz nada
            }

        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }

}
