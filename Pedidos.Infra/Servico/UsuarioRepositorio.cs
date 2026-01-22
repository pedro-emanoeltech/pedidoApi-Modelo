using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.Entidades;
using Pedidos.Infra.Contexto;
using Pedidos.Infra.Interface;

namespace Pedidos.Infra.Servico
{
    public class UsuarioRepositorio : BaseRepositorio<Usuario, PedidosContexto>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(PedidosContexto contexto) : base(contexto)
        {
        }
 
        public async Task<Usuario?> ObterUsuarioPorEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate: x => x.Email.ToLower() == email.ToLower());
        }
    }
}
