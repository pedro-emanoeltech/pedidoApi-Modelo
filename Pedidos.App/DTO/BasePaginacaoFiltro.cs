using Pedidos.Domain.Abastacao;

namespace Pedidos.App.DTO
{
    /// <summary>
    /// Define propriedades comuns para filtros com paginação.
    /// </summary>
    public abstract class BasePaginacaoFiltro : IBaseDto
    {
        /// <summary>
        /// Número da página (1 por padrão).
        /// </summary>
        public int Pagina { get; set; } = 1;

        /// <summary>
        /// Quantidade de itens por página (10 por padrão).
        /// </summary>
        public int TamanhoPagina { get; set; } = 10;
    }

}
