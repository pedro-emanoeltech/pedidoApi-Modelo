using Pedidos.Domain.Abastacao;
 
namespace Pedidos.Infra.Interface
{
    public interface IBaseRepositorio<TEntidade> where TEntidade : BaseEntidade, IEntidade
    {
        Task<TEntidade> AdicionarAsync(TEntidade entidade, bool saveChanges = true);
        Task<TEntidade> AtualizarAsync(TEntidade entidade, bool saveChanges = true);
        Task<TEntidade?> ObterPorIdAsync(Guid id);

    }
}
