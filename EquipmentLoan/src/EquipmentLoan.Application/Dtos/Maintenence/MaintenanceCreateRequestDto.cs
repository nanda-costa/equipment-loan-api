using System.ComponentModel.DataAnnotations;

namespace EquipmentLoan.Application.Dtos.Maintenence;

public class MaintenanceCreateRequestDto
{
    [Required(ErrorMessage = "O ID do equipamento é obrigatório.")]
    public Guid EquipmentId { get; set; }

    [Required(ErrorMessage = "A descrição do problema é obrigatória.")]
    [StringLength(500, ErrorMessage = "A descrição não pode passar de 500 caracteres.")]
    public string Description { get; set; } = string.Empty;
}