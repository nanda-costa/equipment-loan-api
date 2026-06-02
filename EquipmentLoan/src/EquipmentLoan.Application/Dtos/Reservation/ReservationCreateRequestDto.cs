using System.ComponentModel.DataAnnotations;

namespace EquipmentLoan.Application.Dtos.Reservation;

public class ReservationCreateRequestDto
{
    [Required(ErrorMessage = "O ID do equipamento é obrigatório.")]
    public Guid EquipmentId { get; set; }

    [Required(ErrorMessage = "O ID do utilizador é obrigatório.")]
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "A data/hora de início é obrigatória.")]
    public DateTime StartTime { get; set; }

    [Required(ErrorMessage = "A data/hora de fim é obrigatória.")]
    public DateTime EndTime { get; set; }
}