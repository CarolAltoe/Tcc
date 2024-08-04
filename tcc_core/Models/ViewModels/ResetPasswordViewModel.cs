using System.ComponentModel.DataAnnotations;

namespace tcc_core.Models.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string NovaSenha { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare("NovaSenha", ErrorMessage = "A nova senha e a confirmação da senha não são idênticas.")]
        public string ConfirmacaoSenha { get; set; } = string.Empty;

        [Required]
        public string Cpf { get; set; } = string.Empty;
    }

}
