using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.Entidades;
using Pedidos.Infra.Map;

namespace Pedidos.Infra.Contexto
{
    public class PedidosContexto : BaseContexto
    {
        public PedidosContexto(DbContextOptions options) : base(options)
        {
      
        }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //implementar reflexão para carregar todos os mapas automaticamente
            modelBuilder.ApplyConfiguration(new PedidoMap());
            modelBuilder.ApplyConfiguration(new ItemPedidoMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
        }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
