using System.ComponentModel.DataAnnotations;

namespace EquipmentLoan.Application.Dtos.Reservation;

public class ReservationCancelRequestDto
{
    [Required(ErrorMessage = "O ID da reserva é obrigatório.")]
    public Guid ReservationId { get; set; }
}