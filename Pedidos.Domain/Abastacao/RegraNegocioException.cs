namespace Pedidos.Domain.Abastacao
{
    /// <summary>
    /// abastracao para violação na Regra Negocio.
    /// </summary>
    public class RegraNegocioException : Exception
    {
        public RegraNegocioException() { }
        public RegraNegocioException(string message) : base(message) { }
    }
}



