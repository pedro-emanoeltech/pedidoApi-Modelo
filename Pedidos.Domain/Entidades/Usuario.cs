using Pedidos.Domain.Abastacao;
using System.ComponentModel.DataAnnotations;

namespace Pedidos.Domain.Entidades
{

    public class Usuario : BaseEntidade
    {
        public Usuario()
        {
            DataCadastro = DateTime.Now;
        }

        public string? Nome { get; set; }


        public string? Email { get; set; }


        public string? Senha { get; set; }
  
        public DateTime DataCadastro { get; set; }

        public void SenhaValida()
        {
            if (string.IsNullOrWhiteSpace(Senha) || Senha.Length < 8 ||
                !Senha.Any(char.IsUpper) || !Senha.Any(char.IsLower) ||
                !Senha.Any(char.IsDigit) || !Senha.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                throw new RegraNegocioException("A senha deve ter no mínimo 8 caracteres, incluindo letras maiúsculas, minúsculas, números e símbolos especiais.");
            }
        }

        public void EmailValido()
        {
            if (string.IsNullOrWhiteSpace(Email) || !new EmailAddressAttribute().IsValid(Email))
            {
                throw new RegraNegocioException("O email fornecido é inválido.");
            }
        }
        public void CriptografarSenha()
        {
            Senha = BCrypt.Net.BCrypt.HashPassword(Senha);
        }
 
    }
}
