using Pedidos.Domain.Abastacao;

namespace Pedidos.App.Interface
{
    public interface IBaseService<TEntidade, TDtoRequisicao, TDtoResposta>
        where TEntidade : BaseEntidade, IEntidade
        where TDtoRequisicao : IBaseDto
    {
        Task<TDtoResposta> AdicionarAsync(TDtoRequisicao dtoRequisicao);
        Task<TDtoResposta> AtualizarAsync(Guid id, TDtoRequisicao dtoRequisicao);
        Task<TDtoResposta> ObterPorIdAsync(Guid id);
 
    }
}
