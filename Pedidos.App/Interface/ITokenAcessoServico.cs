using Pedidos.App.DTO.DtoModelo;
using Pedidos.Domain.Entidades;
using System.Security.Claims;

namespace Pedidos.App.Interface
{
    public interface ITokenAcessoServico
    {
        Task<TokenAcesso> GerarTokenAcessoAsync(Usuario usuario);
        ClaimsPrincipal ValidateJwtToken(string token);

    }
}
