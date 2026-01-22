using Pedidos.Domain.Entidades;

namespace Pedidos.Testes.Entidade
{
    public class PedidoCenario
    {
        public static IEnumerable<object[]> ItensComValores()
        {
            yield return new object[]
            {
            new List<ItemPedido>
            {
                new ItemPedido("Produto X", 3, 15m),
                new ItemPedido("Produto Y", 2, 5m),
                new ItemPedido("Produto Z", 1, 20m)
            },
            75m 
            };

            yield return new object[]
            {
            new List<ItemPedido>
            {
                new ItemPedido("Produto A", 1, 10m),
                new ItemPedido("Produto B", 4, 2.5m)
            },
            20m 
            };
        }
    }
}