using System.ComponentModel.DataAnnotations;

namespace ProcessoSeletivo.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O E-mail fornecido não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        public string? Nome { get; set; }
        public string? FotoPerfil { get; set; }
    }
}
