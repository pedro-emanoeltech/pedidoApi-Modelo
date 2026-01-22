using AutoMapper;
using FluentValidation;
using Pedidos.App.DTO.DtoRequisicao;
using Pedidos.App.DTO.DtoRespostas;
using Pedidos.App.Interface;
using Pedidos.Domain.Abastacao;
using Pedidos.Domain.Entidades;
using Pedidos.Infra.Interface;

namespace Pedidos.App.Servico
{
    public class UsuarioServico : BaseServico<Usuario, UsuarioRequisicao, UsuarioResposta>, IUsuarioServico
    {
        private readonly IUsuarioRepositorio _repositorio;
        public UsuarioServico(
            IMapper mapper,
            IUsuarioRepositorio repositorio,
            IValidator<UsuarioRequisicao> validator)
            : base(repositorio, mapper, validator)
        {
            _repositorio = repositorio;
        }

        public override async Task<UsuarioResposta> AdicionarAsync(UsuarioRequisicao dtoRequisicao)
        {
            var resultadoValidacao = await _validator.ValidateAsync(dtoRequisicao);
            if (!resultadoValidacao.IsValid)
                throw new ValidationException(string.Join("; ", resultadoValidacao.Errors.Select(e => e.ErrorMessage)));

            await ValidarEmailExistenteAsync(dtoRequisicao.Email);

            var entidade = _mapper.Map<Usuario>(dtoRequisicao);
            entidade.SenhaValida();
            entidade.EmailValido();
            entidade.CriptografarSenha();

            await _repositorio.AdicionarAsync(entidade);

            return _mapper.Map<UsuarioResposta>(entidade);
        }

        private async Task ValidarEmailExistenteAsync(string email, Guid? excetoId = null)
        {
            var existe = await _repositorio.ObterUsuarioPorEmailAsync(email);

            if (existe != null && existe.Id != excetoId)
                throw new RegraNegocioException("O email fornecido já está em uso.");
        }
    }
}
