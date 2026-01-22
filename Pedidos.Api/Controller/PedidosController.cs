using Microsoft.AspNetCore.Mvc;
using Pedidos.App.DTO.DtoRequisicao;
using Pedidos.App.DTO.DtoRespostas;
using Pedidos.App.Interface;
using Pedidos.Domain.Entidades;
using Pedidos.Domain.Enum;


namespace Pedidos.Api.Controller
{
    public class PedidosController : BaseCRUDController<Pedido, PedidoRequisicao, PedidoResposta>
    {
        private readonly IPedidoServico _servico;
        public PedidosController(IPedidoServico servico, ILogger<PedidosController> logger) : base(servico, logger)
        {
            _servico = servico;
        }

        #region CRUD

        /// <summary>
        /// Cria um novo pedido.
        /// </summary>
        /// <remarks>
        /// **Exemplo de requisição:**
        /// ```json
        /// {
        ///   "clienteNome": "Pedro Emanoel",
        ///   "itens": [
        ///     {
        ///       "produtoNome": "Notebook",
        ///       "quantidade": 1,
        ///       "precoUnitario": 3500.00
        ///     }
        ///   ]
        /// }
        /// ```
        ///
        /// **Regras:**
        /// - O pedido deve conter ao menos um item.
        /// - Quantidade e preço unitário devem ser maiores que zero.
        /// - O pedido é criado com status **Novo** por padrão.
        /// </remarks>
        /// <param name="requisicao">Dados do pedido a ser criado.</param>
        /// <returns>Pedido criado.</returns>
        /// <response code="201">Pedido criado com sucesso.</response>
        /// <response code="400">Modelo de dados inválido.</response>
        /// <response code="500">Erro interno ao processar a solicitação.</response>
        [HttpPost]
        public override async Task<ActionResult<PedidoResposta>> Adicionar([FromBody] PedidoRequisicao requisicao)
        {
            try
            {
                return await base.Adicionar(requisicao);
            }
            catch (Exception ex)
            {
                return ValidaException(ex);
            }
        }
 
        /// <summary>
        /// Obtém um pedido Id.
        /// </summary>
        /// <param name="id">Id do pedido.</param>
        /// <returns>Pedido com seus itens.</returns>
        /// <response code="200">Pedido encontrado.</response>
        /// <response code="404">Pedido não encontrado.</response>
        /// <response code="500">Erro interno.</response>
        [HttpGet("{id}")]
        public override async Task<ActionResult<PedidoResposta>> ObterPorIdAsync(string id)
        {
            return await base.ObterPorIdAsync(id);
        }

        /// <summary>
        /// Lista pedidos, com filtro opcional por status.
        /// </summary>
        /// <param name="status">Status do pedido (Novo, Pago, Cancelado).</param>
        /// <returns>Lista de pedidos.</returns>
        /// <response code="200">Lista retornada com sucesso.</response>
        /// <response code="500">Erro interno.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoResposta>>> ObterPorStatus([FromQuery] PedidoStatus? status)
        {
            var pedidos = await _servico.ObterPorStatusAsync(status);
            return Ok(pedidos);
        }


        /// <summary>
        /// Cancela um pedido.
        /// </summary>
        /// <remarks>
        /// **Regra de negócio:**
        /// - Não é permitido cancelar um pedido com status **Pago**.
        /// </remarks>
        /// <param name="id">Id do pedido.</param>
        /// <response code="204">Pedido cancelado com sucesso.</response>
        /// <response code="400">Pedido pago não pode ser cancelado.</response>
        /// <response code="404">Pedido não encontrado.</response>
        /// <response code="500">Erro interno.</response>
        [HttpPut("{id}/cancelar")]
        public async Task<IActionResult> Cancelar(Guid id)
        {
            try
            {
                await _servico.CancelarAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }


        #endregion

    }
}
