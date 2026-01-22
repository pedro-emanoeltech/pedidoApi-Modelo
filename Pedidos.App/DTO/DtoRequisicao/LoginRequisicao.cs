using FluentValidation;
using Pedidos.Domain.Abastacao;

namespace Pedidos.App.DTO.DtoRequisicao
{
    public class LoginRequisicao : IBaseDto
    {
        /// <summary>
        /// Obtém ou define o Email da conta.
        /// </summary>
        /// <example>fulano@qq.com</example>
        public string? Email { get; set; }

        public string? Senha { get; set; }

        public class LoginRequisicaoValidator : AbstractValidator<LoginRequisicao>
        {
            public LoginRequisicaoValidator()
            {
                RuleFor(x => x.Email)
                       .NotEmpty().WithMessage("É obrigatório informar o email")
                       .EmailAddress().WithMessage("Email inválido");

                RuleFor(banco => banco.Senha)
                    .NotNull().NotEmpty().WithMessage("É obrigatório informar senha do usuario.");

            }
        }
    }
}
