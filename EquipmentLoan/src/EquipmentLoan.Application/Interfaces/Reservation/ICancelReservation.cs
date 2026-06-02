using EquipmentLoan.Application.Dtos.Reservation;

namespace EquipmentLoan.Application.Interfaces.Reservation;

public interface ICancelReservation
{
    public Task<ReservationResponseDto> Execute(ReservationCancelRequestDto requestDto);
}