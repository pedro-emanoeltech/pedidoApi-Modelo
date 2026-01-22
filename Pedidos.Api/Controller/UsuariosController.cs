using Microsoft.AspNetCore.Mvc;
using Pedidos.App.DTO.DtoRequisicao;
using Pedidos.App.DTO.DtoRespostas;
using Pedidos.App.Interface;
using Pedidos.Domain.Entidades;


namespace Pedidos.Api.Controller
{
    public class UsuariosController : BaseCRUDController<Usuario, UsuarioRequisicao, UsuarioResposta>
    {
        private readonly IUsuarioServico _servico;
        public UsuariosController(IUsuarioServico servico, ILogger<UsuariosController> logger) : base(servico, logger)
        {
            _servico = servico;
        }

        #region CRUD

        /// <summary>
        /// Adiciona um novo usuário.
        /// </summary>
        /// <remarks>
        /// **Exemplo de requisição:**
        /// ```json
        /// {
        ///   "nome": "Pedro Emanoel",
        ///   "email": "Pedrao.emanoel@exemplo.com",
        ///   "senha": "SenhaForte@123"
        /// }
        /// ```
        /// **Descrição dos campos:**
        /// - `nome` → Nome completo (mínimo 3 caracteres).
        /// - `email` → Email válido.
        /// - `senha` → Senha forte (mínimo 8 caracteres, com letras maiúsculas, minúsculas, número e símbolo especial).
        /// </remarks>
        /// <param name="usuarioRequisicao">body contendo os dados do usuário a ser criado.</param>
        /// <returns>Retorna o usuário criado, com todas as propriedades preenchidas.</returns>
        /// <response code="201">Usuário criado com sucesso.</response>
        /// <response code="400">Modelo de dados inválido</response>
        /// <response code="500">Erro interno ao processar a solicitação.</response>
        [HttpPost]
        public override async Task<ActionResult<UsuarioResposta>> Adicionar([FromBody] UsuarioRequisicao usuarioRequisicao)
        {
            try
            {
                return await _servico.AdicionarAsync(usuarioRequisicao);
            }
            catch (Exception ex)
            {
                return ValidaException(ex);
            }

        }

        /// <summary>
        /// Busca um usuário pelo seu Id.
        /// </summary>
        /// <param name="id">Id do usuário.</param>
        /// <returns>Retorna o usuário encontrado.</returns>
        /// <response code="200">Usuário encontrado.</response>
        /// <response code="400">Id inválido.</response>
        /// <response code="404">Usuário não encontrado.</response>
        /// <response code="500">Erro interno ao processar a solicitação.</response>
        [HttpGet("{id}", Name = "BuscarUsuario")]
        public override async Task<ActionResult<UsuarioResposta>> ObterPorIdAsync(string id)
        {
            return await base.ObterPorIdAsync(id);
        }
 
        #endregion

    }
}
