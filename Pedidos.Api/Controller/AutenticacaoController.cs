using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pedidos.App.DTO.DtoRequisicao;
using Pedidos.App.DTO.DtoRespostas;
using Pedidos.App.Interface;


namespace Pedidos.Api.Controller
{
    public class AutenticacaoController : BaseController
    {
        private readonly IAutenticacaoServico _servico;

        public AutenticacaoController(IAutenticacaoServico servico, ILogger<AutenticacaoController> logger) : base(logger)
        {
            _servico = servico;
        }

        /// <summary>
        /// Realiza o login de um usuário na API e retorna o token de autenticação JWT.
        /// </summary>
        /// <remarks>
        /// **Exemplo de Requisição:**
        /// ```json
        /// {
        ///     "email": "fulano@qq.com",
        ///     "senha": "Bt-2aaaB@1"
        /// }
        /// ```
        /// **Exemplo de Resposta de Sucesso (200 OK):**
        /// ```json
        /// {
        ///   "Token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NSIsImV4cCI6MTYyMzY0Nzg5OX0",
        ///   "DataExpiracao": "2025-02-05T11:20:25.0532374-03:00"
        /// }
        /// ```
        /// **Exemplo de Resposta de Falha (401 Unauthorized):**
        /// ```json
        /// {
        ///   "Mensagem": "Email ou senha inválidos."
        /// }
        /// ```
        /// </remarks>
        /// <param name="loginRequisicao">Objeto contendo o email e a senha do usuário.</param>
        /// <returns>Retorna o token JWT e a data de expiração em caso de sucesso.</returns>
        /// <response code="200">Login efetuado com sucesso. Retorna o token de autenticação.</response>
        /// <response code="400">Modelo de dados inválido ou campos obrigatórios ausentes.</response>
        /// <response code="401">Falha na autenticação do usuário (email ou senha inválidos).</response>
        /// <response code="500">Erro interno ao processar a solicitação.</response>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResposta>> Login([FromBody] LoginRequisicao loginRequisicao)
        {
            if (loginRequisicao == null)
                return BadRequest("Email e/ou senha inválidos");

            try
            {
                var resposta = await _servico.LoginAsync(loginRequisicao);
                return Ok(resposta);
            }
            catch (Exception ex)
            {
                return ValidaException(ex);
            }
        }
    }
}
