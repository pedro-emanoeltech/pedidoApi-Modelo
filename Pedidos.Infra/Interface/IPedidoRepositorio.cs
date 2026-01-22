using Pedidos.Domain.Entidades;
using Pedidos.Domain.Enum;

namespace Pedidos.Infra.Interface
{
    public interface IPedidoRepositorio : IBaseRepositorio<Pedido>
    {
        Task<Pedido?> ObterComItensAsync(Guid id);
        Task<IEnumerable<Pedido>> ObterPorStatusAsync(PedidoStatus? status);
    }
}
