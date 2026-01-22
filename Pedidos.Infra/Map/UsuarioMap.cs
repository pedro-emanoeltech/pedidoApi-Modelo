using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pedidos.Domain.Entidades;

namespace Pedidos.Infra.Map
{
    public class UsuarioMap : BaseEntidadeMap<Usuario>
    {
        public override void Configure(EntityTypeBuilder<Usuario> builder)
        {
            base.Configure(builder);
 
            builder.Property(s => s.Nome)
                   .IsRequired()
                   .HasMaxLength(150);
 
            builder.Property(s => s.Email)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(s => s.Senha)
                   .IsRequired()
                   .HasMaxLength(500);
 
 
        }
    }
}
