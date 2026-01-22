using Pedidos.Domain.Entidades;

namespace Pedidos.Infra.Interface
{
    public interface IUsuarioRepositorio : IBaseRepositorio<Usuario>
    {
        Task<Usuario?> ObterUsuarioPorEmailAsync(string email);
    }
}
