using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.Entidades;
using Pedidos.Domain.Enum;
using Pedidos.Infra.Contexto;
using Pedidos.Infra.Interface;
using System;

namespace Pedidos.Infra.Servico
{
    public class PedidoRepositorio : BaseRepositorio<Pedido, PedidosContexto>, IPedidoRepositorio
    {
        private readonly PedidosContexto _contexto;
 
        public PedidoRepositorio(PedidosContexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }
 
        public async Task<Pedido?> ObterComItensAsync(Guid id)
        {
            return await _contexto.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Pedido>> ObterPorStatusAsync(PedidoStatus? status)
        {
            var query = _contexto.Pedidos.AsQueryable();

            if (status.HasValue)
                query = query.Where(p => p.Status == status);

            return await query
                .Include(p => p.Itens)
                .ToListAsync();
        }

    }
}
