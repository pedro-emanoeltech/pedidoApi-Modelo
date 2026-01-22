using Pedidos.Domain.Abastacao;

namespace Pedidos.Domain.Entidades
{
 
    public class ItemPedido : BaseEntidade
    {
        public Guid PedidoId { get; private set; }
        public string ProdutoNome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal PrecoUnitario { get; private set; }

        protected ItemPedido() { }

        public ItemPedido(string produtoNome, int quantidade, decimal precoUnitario)
        {
            ProdutoNome = produtoNome;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
        }

        public void AssociarPedido(Pedido pedido)
        {
            Pedido = pedido;
            PedidoId = pedido.Id.Value;
        }

        public Pedido Pedido { get; private set; }

    }
}
