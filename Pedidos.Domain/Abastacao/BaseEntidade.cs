using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos.Domain.Abastacao
{
    /// <summary>
    /// Entidade com a chave type <see cref="Guid"/>.
    /// </summary>
    public abstract class BaseEntidade : IEntidade
    {
        /// <summary>
        /// Obtém ou define o ID do registro.
        /// </summary>

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid? Id { get; set; }
        protected BaseEntidade()
        {
            Id = Guid.NewGuid();
        }
    }
}



