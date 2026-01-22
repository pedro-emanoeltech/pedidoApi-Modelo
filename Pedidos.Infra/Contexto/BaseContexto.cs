using Microsoft.EntityFrameworkCore;

namespace Pedidos.Infra.Contexto
{
    public class BaseContexto : DbContext
    {
        public BaseContexto(DbContextOptions options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", isEnabled: true);
        }
    }
}
