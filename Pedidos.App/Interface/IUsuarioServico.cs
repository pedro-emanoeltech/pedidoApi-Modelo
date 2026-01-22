using Pedidos.App.DTO.DtoRequisicao;
using Pedidos.App.DTO.DtoRespostas;
using Pedidos.Domain.Entidades;

namespace Pedidos.App.Interface
{
    public interface IUsuarioServico : IBaseService<Usuario, UsuarioRequisicao, UsuarioResposta>
    {
        Task<UsuarioResposta> AdicionarAsync(UsuarioRequisicao dtoRequisicao);
    }
}
