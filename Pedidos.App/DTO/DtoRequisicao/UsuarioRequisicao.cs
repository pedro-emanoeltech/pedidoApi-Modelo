using FluentValidation;

namespace Pedidos.App.DTO.DtoRequisicao
{
    public class UsuarioRequisicao : BaseUsuarioRequisicao
    {
 
        public class UsuarioRequisicaoValidator : AbstractValidator<UsuarioRequisicao>
        {
            public UsuarioRequisicaoValidator()
            {
                var baseValidator = new BaseUsuarioRequisicaoValidator();
                Include(baseValidator);
 
            }
        }
    }
}
