using Pedidos.App.DTO.DtoRequisicao;
using Pedidos.App.DTO.DtoRespostas;
using Pedidos.Domain.Entidades;
using Pedidos.Domain.Enum;

namespace Pedidos.App.Interface
{
    public interface IPedidoServico : IBaseService<Pedido, PedidoRequisicao, PedidoResposta>
    {
        Task<PedidoResposta?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<PedidoResposta>> ObterPorStatusAsync(PedidoStatus? status);
        Task CancelarAsync(Guid id);
    }
}
