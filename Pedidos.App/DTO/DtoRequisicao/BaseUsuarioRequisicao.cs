using FluentValidation;
using Pedidos.Domain.Abastacao;

namespace Pedidos.App.DTO.DtoRequisicao
{
    public class BaseUsuarioRequisicao : IBaseDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public class BaseUsuarioRequisicaoValidator : AbstractValidator<BaseUsuarioRequisicao>
        {
            public BaseUsuarioRequisicaoValidator()
            {
                RuleFor(x => x.Nome)
                    .NotEmpty().WithMessage("É obrigatório informar o Nome")
                    .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres");

                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("É obrigatório informar o email")
                    .EmailAddress().WithMessage("Email inválido");

                RuleFor(x => x.Senha)
                    .NotEmpty().WithMessage("Senha é obrigatória")
                    .MinimumLength(8).WithMessage("Senha deve ter pelo menos 8 caracteres")
                    .Matches("[A-Z]").WithMessage("Senha deve conter ao menos uma letra maiúscula")
                    .Matches("[a-z]").WithMessage("Senha deve conter ao menos uma letra minúscula")
                    .Matches("[0-9]").WithMessage("Senha deve conter ao menos um número")
                    .Matches(@"[\!\@\#\$\%\^\&\*\(\)\-\+]").WithMessage("Senha deve conter ao menos um símbolo especial");
            }
        }
    }
}
