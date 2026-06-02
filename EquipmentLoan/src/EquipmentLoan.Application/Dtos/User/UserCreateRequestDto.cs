using System.ComponentModel.DataAnnotations;
using EquipmentLoan.Domain.Enums;

namespace EquipmentLoan.Application.DTOs
{
    public class UserCreateRequestDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode passar de 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 50 caracteres.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "O perfil (Role) do usuário é obrigatório.")]
        public Roles Role { get; set; }
    }
}