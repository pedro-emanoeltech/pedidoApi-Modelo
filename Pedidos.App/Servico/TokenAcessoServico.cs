using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pedidos.App.DTO.DtoModelo;
using Pedidos.App.Interface;
using Pedidos.Domain.Entidades;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pedidos.App.Servico
{
    public class TokenAcessoServico : ITokenAcessoServico
    {
        private readonly IConfiguration _configuration;

        public TokenAcessoServico(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<TokenAcesso> GerarTokenAcessoAsync(Usuario usuario)
        {
            try
            {
                var dataExpiracao = DateTime.UtcNow.AddHours(8);
                var secretKey = _configuration["JwtConfig:SecretKey"];
                var issuer = _configuration["JwtConfig:Issuer"];
                var audience = _configuration["JwtConfig:Audience"];

                var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey!));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
 
                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, usuario.Id.ToString()!),
                    new(ClaimTypes.Email, usuario.Email!.ToString())
                };

                var token = new JwtSecurityToken(
                    issuer,
                    audience,
                    claims,
                    expires: dataExpiracao,
                    signingCredentials: credentials
                );


                return new TokenAcesso
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    DataExpiracao = dataExpiracao
                };
            }
            catch (Exception e)
            {

                throw;
            }
            
        }

        public ClaimsPrincipal ValidateJwtToken(string token)
        {
            var securityKey = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("SecretKey")!);

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Environment.GetEnvironmentVariable("Issuer"),
                    ValidAudience = Environment.GetEnvironmentVariable("Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(securityKey)
                }, out SecurityToken validatedToken);

                return claimsPrincipal;
            }
            catch (SecurityTokenExpiredException)
            {
                throw new ApplicationException("Token de acesso Expirou.");
            }
        }

    }
}
