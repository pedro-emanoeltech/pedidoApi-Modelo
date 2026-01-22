using Pedidos.App.DTO.DtoRequisicao;
using Pedidos.App.DTO.DtoRespostas;

namespace Pedidos.App.Interface
{
    public interface IAutenticacaoServico
    {
        Task<LoginResposta> LoginAsync(LoginRequisicao loginRequisicao);
    }
}
