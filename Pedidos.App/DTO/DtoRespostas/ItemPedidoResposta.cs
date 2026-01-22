using Pedidos.Domain.Abastacao;

namespace Pedidos.App.DTO.DtoRespostas
{
    public class ItemPedidoResposta : IBaseDto
    {
        public Guid Id { get; set; }
        public string ProdutoNome { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal => Quantidade * PrecoUnitario;
    }
}
