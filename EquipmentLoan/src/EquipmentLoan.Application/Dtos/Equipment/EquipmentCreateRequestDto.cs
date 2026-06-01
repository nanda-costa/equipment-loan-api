using System.ComponentModel.DataAnnotations;

namespace EquipmentLoan.Application.DTOs
{
    public class EquipmentCreateRequestDto
    {
        [Required(ErrorMessage = "O nome do equipamento é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode passar de 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O número de série é obrigatório.")]
        [StringLength(50, ErrorMessage = "O número de série não pode passar de 50 caracteres.")]
        public string SerialNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "A categoria do equipamento é obrigatória.")]
        public Guid CategoryId { get; set; }
    }
}