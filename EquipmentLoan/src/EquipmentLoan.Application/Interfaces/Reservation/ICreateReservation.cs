using EquipmentLoan.Application.Dtos.Reservation;

namespace EquipmentLoan.Application.Interfaces.Reservation;

public interface ICreateReservation
{
    public Task<ReservationResponseDto> Execute(ReservationCreateRequestDto requestDto);
}