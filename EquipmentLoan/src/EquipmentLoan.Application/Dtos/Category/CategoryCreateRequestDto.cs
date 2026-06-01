using System.ComponentModel.DataAnnotations;

namespace EquipmentLoan.Application.DTOs
{
    public class CategoryCreateRequestDto
    {
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode passar de 100 caracteres.")]
        public required string Name { get; set; } = string.Empty;

        [StringLength(250, ErrorMessage = "A descrição não pode passar de 250 caracteres.")]
        public string Description { get; set; } = string.Empty;
    }
}