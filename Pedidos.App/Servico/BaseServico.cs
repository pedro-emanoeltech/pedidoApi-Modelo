using AutoMapper;
using FluentValidation;
using Pedidos.App.Interface;
using Pedidos.Domain.Abastacao;
using Pedidos.Infra.Interface;

namespace Pedidos.App.Servico
{
    public abstract class BaseServico<TEntidade, TDtoRequisicao, TDtoResposta> : IBaseService<TEntidade, TDtoRequisicao, TDtoResposta>
        where TEntidade : BaseEntidade, IEntidade
        where TDtoRequisicao : IBaseDto
        where TDtoResposta : IBaseDto
    {

        protected readonly IMapper _mapper;
        private readonly IBaseRepositorio<TEntidade> _repositorio;
        protected readonly IValidator<TDtoRequisicao> _validator;

        protected BaseServico(IBaseRepositorio<TEntidade> repositorio, IMapper mapper, IValidator<TDtoRequisicao> validator)
        {
            _repositorio = repositorio;
            _mapper = mapper;
            _validator = validator;
        }


        public virtual async Task<TDtoResposta> AdicionarAsync(TDtoRequisicao dtoRequisicao)
        {
            var resultadoValidacao = await _validator.ValidateAsync(dtoRequisicao);
            if (!resultadoValidacao.IsValid)
                throw new ValidationException(string.Join("; ", resultadoValidacao.Errors.Select(e => e.ErrorMessage)));

            var entidade = _mapper.Map<TEntidade>(dtoRequisicao);

            await _repositorio.AdicionarAsync(entidade);

            return _mapper.Map<TDtoResposta>(entidade);
        }

        public virtual async Task<TDtoResposta> AtualizarAsync(Guid id, TDtoRequisicao dtoRequisicao)
        {
            var resultadoValidacao = await _validator.ValidateAsync(dtoRequisicao);
            if (!resultadoValidacao.IsValid)
                throw new ValidationException(string.Join("; ", resultadoValidacao.Errors.Select(e => e.ErrorMessage)));

            var entidade = await _repositorio.ObterPorIdAsync(id)
                      ?? throw new NotFoundException("Registro não encontrado");

            _mapper.Map(dtoRequisicao, entidade);

            await _repositorio.AtualizarAsync(entidade);

            return _mapper.Map<TDtoResposta>(entidade);
        }

        public virtual async Task<TDtoResposta> ObterPorIdAsync(Guid id)
        {
            var query = await _repositorio.ObterPorIdAsync(id);

            return _mapper.Map<TDtoResposta>(query) ?? throw new NotFoundException("Registro não encontrado");
        }
 
    }

}

