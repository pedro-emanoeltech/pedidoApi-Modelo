using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.Abastacao;

namespace Pedidos.App.DTO
{
    /// <summary>
    /// Representa o resultado paginada.
    /// </summary>
    /// <typeparam name="T">Tipo do item retornado. que herda de base dto</typeparam>
    public class ResultadoPaginado<T> where T : IBaseDto
    {
        /// <summary>
        /// Itens da página atual.
        /// </summary>
        public IEnumerable<T> Items { get; set; } = [];

        /// <summary>
        /// Número da página atual.
        /// </summary>
        public int NumeroPaginas { get; set; }

        /// <summary>
        /// Quantidade de itens por página.
        /// </summary>
        public int TamanhoPagina { get; set; }

        /// <summary>
        /// Total de registros encontrados.
        /// </summary>
        public int TotalItens { get; set; }

        /// <summary>
        /// Total de páginas
        /// </summary>
        public int TotalPagina => (int)Math.Ceiling((double)TotalItens / TamanhoPagina);

        public ResultadoPaginado() { }

        public ResultadoPaginado(IEnumerable<T> items, int totalItens, int numeroPaginas, int tamanhoPagina)
        {
            Items = items;
            TotalItens = totalItens;
            NumeroPaginas = numeroPaginas;
            TamanhoPagina = tamanhoPagina;
        }

        public static async Task<ResultadoPaginado<T>> CriarAsync(
            IQueryable<T> query,
            BasePaginacaoFiltro filtro)
        {
            var totalRegistros = await query.CountAsync();

            var skip = (filtro.Pagina - 1) * filtro.TamanhoPagina;
            var items = await query.Skip(skip).Take(filtro.TamanhoPagina).ToListAsync();

            return new ResultadoPaginado<T>(items, totalRegistros, filtro.Pagina, filtro.TamanhoPagina);
        }
    }

}



