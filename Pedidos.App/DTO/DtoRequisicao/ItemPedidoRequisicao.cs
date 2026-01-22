using FluentValidation;
using Pedidos.Domain.Abastacao;

namespace Pedidos.App.DTO.DtoRequisicao
{
    public class ItemPedidoRequisicao : IBaseDto
    {
        public string ProdutoNome { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }

        public class ItemPedidoRequisicaoValidator : AbstractValidator<ItemPedidoRequisicao>
        {
            public ItemPedidoRequisicaoValidator()
            {
                RuleFor(x => x.ProdutoNome)
                    .NotEmpty().WithMessage("Nome do produto obrigatorio")
                    .MaximumLength(200);

                RuleFor(x => x.Quantidade)
                    .GreaterThan(0).WithMessage("quantidade deve ser maior que zero");

                RuleFor(x => x.PrecoUnitario)
                    .GreaterThan(0).WithMessage("preço unitario deve ser maior que zero");
            }
        }
    }
}
