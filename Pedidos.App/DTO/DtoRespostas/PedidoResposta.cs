using Pedidos.Domain.Abastacao;

namespace Pedidos.App.DTO.DtoRespostas
{
    public class PedidoResposta : IBaseDto
    {
        public Guid Id { get; set; }
        public string ClienteNome { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Status { get; set; }
        public decimal ValorTotal { get; set; }

        public List<ItemPedidoResposta> Itens { get; set; } = new();
    }
}
