using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pedidos.Domain.Entidades;
using Pedidos.Domain.Enum;

namespace Pedidos.Infra.Map
{
    public class PedidoMap : BaseEntidadeMap<Pedido>
    {
        public override void Configure(EntityTypeBuilder<Pedido> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.ClienteNome)
                .HasMaxLength(250)
                .IsRequired();
 
            builder.Property(p => p.Status)
                .HasConversion(new EnumToStringConverter<PedidoStatus>())
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.ValorTotal)
                .HasColumnType("numeric(18,2)")
                .IsRequired();

            builder.HasMany(p => p.Itens)
                .WithOne(i => i.Pedido)
                .HasForeignKey(i => i.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
