using System.ComponentModel.DataAnnotations;

namespace tcc_core.Models.ViewModels
{
    public class UsuarioEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo E-mail deve ter um endereço de e-mail válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string Cpf { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        public string? NovaSenha { get; set; } = string.Empty;


    }
}
