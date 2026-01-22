using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pedidos.Domain.Entidades;

namespace Pedidos.Infra.Map
{
    public class ItemPedidoMap : BaseEntidadeMap<ItemPedido>
    {
        public override void Configure(EntityTypeBuilder<ItemPedido> builder)
        {
            base.Configure(builder);
 
            builder.Property(i => i.ProdutoNome)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(i => i.Quantidade)
                .IsRequired();

            builder.Property(i => i.PrecoUnitario)
                .HasColumnType("numeric(18,2)")
                .IsRequired();

            builder.HasOne(i => i.Pedido)
                .WithMany(p => p.Itens)
                .HasForeignKey(i => i.PedidoId);
        }
    }


}
