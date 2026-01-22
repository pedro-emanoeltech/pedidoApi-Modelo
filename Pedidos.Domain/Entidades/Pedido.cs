using Pedidos.Domain.Abastacao;
using Pedidos.Domain.Enum;

namespace Pedidos.Domain.Entidades
{
 
    public class Pedido : BaseEntidade
    {
        public string ClienteNome { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public PedidoStatus Status { get; private set; }
        public decimal ValorTotal { get; private set; }

        public List<ItemPedido> Itens { get; set; } = new();


        protected Pedido() { }

        public Pedido(string clienteNome, List<ItemPedido> itens)
        {
            ClienteNome = clienteNome;
            DataCriacao = DateTime.UtcNow;
            Status = PedidoStatus.Novo;

            AdicionarItens(itens);
            CalcularValorTotal();
        }

        public void AdicionarItens(IEnumerable<ItemPedido> itens)
        {
            foreach (var item in itens)
            {
                item.AssociarPedido(this);
                Itens.Add(item);
            }
        }

        public void CalcularValorTotal()
        {
            ValorTotal = Itens.Sum(i => i.Quantidade * i.PrecoUnitario);
        }

        public void Cancelar()
        {
            if (Status == PedidoStatus.Pago)
                throw new InvalidOperationException("Pedido pago não pode ser cancelado");

            if (Status == PedidoStatus.Cancelado)
                throw new InvalidOperationException("Pedido já está cancelado");

            Status = PedidoStatus.Cancelado;
        }
    }
}
