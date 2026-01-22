using Pedidos.Domain.Entidades;
using Pedidos.Domain.Enum;

namespace Pedidos.Testes.Entidade
{
    public class PedidoTests
    {
        [Fact]
        public void CriarPedido_DeveDefinirClienteDataStatusEValorTotal()
        {
            var itens = new List<ItemPedido>{ new ItemPedido("Produto A", 2, 10m),new ItemPedido("Produto B", 1, 20m) };

            var pedido = new Pedido("Pedro Emanoel", itens);

            Assert.Equal("Pedro Emanoel", pedido.ClienteNome);
            Assert.Equal(PedidoStatus.Novo, pedido.Status);
            Assert.Equal(40m, pedido.ValorTotal);
            Assert.Equal(2, pedido.Itens.Count);

            foreach (var item in pedido.Itens)
            {
                Assert.Equal(pedido.Id.Value, item.PedidoId);
                Assert.Equal(pedido, item.Pedido);
            }
        }

        [Theory]
        [MemberData(nameof(PedidoCenario.ItensComValores), MemberType = typeof(PedidoCenario))]
        public void CalcularValorTotal_DeveSomarTodosOsItens(List<ItemPedido> itens, decimal valorEsperado)
        {

            var pedido = new Pedido("Cliente Teste", new List<ItemPedido>());
            pedido.AdicionarItens(itens);
            pedido.CalcularValorTotal();


            Assert.Equal(valorEsperado, pedido.ValorTotal);
        }

        [Fact]
        public void Cancelar_DeveAlterarStatusParaCancelado_QuandoPedidoNovo()
        {
            var pedido = new Pedido("Cliente", new List<ItemPedido>());
            pedido.Cancelar();

            Assert.Equal(PedidoStatus.Cancelado, pedido.Status);
        }

        [Fact]
        public void Cancelar_DeveLancarException_QuandoPedidoPago()
        {
            var pedido = new Pedido("Cliente", new List<ItemPedido>());
            typeof(Pedido).GetProperty("Status")?.SetValue(pedido, PedidoStatus.Pago);

            var ex = Assert.Throws<InvalidOperationException>(() => pedido.Cancelar());
            Assert.Equal("Pedido pago não pode ser cancelado", ex.Message);
        }

        [Fact]
        public void Cancelar_DeveLancarException_QuandoPedidoJaCancelado()
        {
            var pedido = new Pedido("Cliente", new List<ItemPedido>());
            typeof(Pedido).GetProperty("Status")?.SetValue(pedido, PedidoStatus.Cancelado);

            var ex = Assert.Throws<InvalidOperationException>(() => pedido.Cancelar());
            Assert.Equal("Pedido já está cancelado", ex.Message);
        }
    }
}