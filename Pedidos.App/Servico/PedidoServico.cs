using AutoMapper;
using FluentValidation;
using Pedidos.App.DTO.DtoRequisicao;
using Pedidos.App.DTO.DtoRespostas;
using Pedidos.App.Interface;
using Pedidos.Domain.Entidades;
using Pedidos.Domain.Enum;
using Pedidos.Infra.Interface;

namespace Pedidos.App.Servico
{
    public class PedidoServico : BaseServico<Pedido, PedidoRequisicao, PedidoResposta>, IPedidoServico
    {
        private readonly IPedidoRepositorio _repositorio;
 
        public PedidoServico(
            IMapper mapper,
            IPedidoRepositorio repositorio,
            IValidator<PedidoRequisicao> validator)
            : base(repositorio, mapper, validator)
        {
            _repositorio = repositorio;
        }
 
        public override async Task<PedidoResposta> AdicionarAsync(PedidoRequisicao dtoRequisicao)
        {
            await _validator.ValidateAndThrowAsync(dtoRequisicao);

            var itens = dtoRequisicao.Itens
                .Select(i => new ItemPedido(
                    i.ProdutoNome,
                    i.Quantidade,
                    i.PrecoUnitario))
                .ToList();

            var pedido = new Pedido(dtoRequisicao.ClienteNome, itens);

            await _repositorio.AdicionarAsync(pedido);

            return _mapper.Map<PedidoResposta>(pedido);
        }

        public async Task<PedidoResposta?> ObterPorIdAsync(Guid id)
        {
            var pedido = await _repositorio.ObterComItensAsync(id);
            return pedido == null ? null : _mapper.Map<PedidoResposta>(pedido);
        }

        public async Task<IEnumerable<PedidoResposta>> ObterPorStatusAsync(PedidoStatus? status)
        {
            var pedidos = await _repositorio.ObterPorStatusAsync(status);
            return _mapper.Map<IEnumerable<PedidoResposta>>(pedidos);
        }

        public async Task CancelarAsync(Guid id)
        {
            var pedido = await _repositorio.ObterPorIdAsync(id)
                ?? throw new KeyNotFoundException("Pedido não encontrado");

            pedido.Cancelar();

            await _repositorio.AtualizarAsync(pedido);
        }

    }
}
