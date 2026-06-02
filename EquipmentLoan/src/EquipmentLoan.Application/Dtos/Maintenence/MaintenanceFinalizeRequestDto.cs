using System.ComponentModel.DataAnnotations;

namespace EquipmentLoan.Application.Dtos.Maintenence;

public class MaintenanceFinalizeRequestDto
{
    [Required(ErrorMessage = "O ID da manutenção é obrigatório.")]
    public Guid MaintenanceId { get; set; }
}