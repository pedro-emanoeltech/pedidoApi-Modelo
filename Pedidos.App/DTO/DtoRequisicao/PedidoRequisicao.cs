using FluentValidation;
using Pedidos.Domain.Abastacao;
using static Pedidos.App.DTO.DtoRequisicao.ItemPedidoRequisicao;

namespace Pedidos.App.DTO.DtoRequisicao
{
    public class PedidoRequisicao : IBaseDto
    {
        public string ClienteNome { get; set; }
        public List<ItemPedidoRequisicao> Itens { get; set; } = new();

        public class PedidoRequisicaoValidator : AbstractValidator<PedidoRequisicao>
        {
            public PedidoRequisicaoValidator()
            {
                RuleFor(x => x.ClienteNome)
                    .NotEmpty().WithMessage("Nome do cliente é obrigatório")
                    .MaximumLength(250);

                RuleFor(x => x.Itens)
                    .NotEmpty().WithMessage("O pedido deve conter ao menos um item");

                RuleForEach(x => x.Itens)
                    .SetValidator(new ItemPedidoRequisicaoValidator());
            }
             
        }
    }
}
