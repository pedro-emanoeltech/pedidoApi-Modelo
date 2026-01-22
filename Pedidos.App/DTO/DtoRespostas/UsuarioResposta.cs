using Pedidos.Domain.Abastacao;

namespace Pedidos.App.DTO.DtoRespostas
{
    public class UsuarioResposta : IBaseDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}
