using EquipmentLoan.Application.Dtos.Reservation;
using EquipmentLoan.Application.Interfaces.Reservation;
using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Interfaces;

namespace EquipmentLoan.Application.Services;

public class CancelReservationService : ICancelReservation
{
    private readonly IReservationRepository _reservationRepository;

    public CancelReservationService(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<ReservationResponseDto> Execute(ReservationCancelRequestDto requestDto)
    {
        Reservation reservation = await _reservationRepository.GetByIdAsync(requestDto.ReservationId);
        if (reservation == null)
            throw new Exception("Reserva não encontrada.");

        if (!reservation.IsActive)
            throw new Exception("Esta reserva já se encontra cancelada.");

        reservation.IsActive = false;
        _reservationRepository.Update(reservation);

        await _reservationRepository.SaveChangesAsync();

        return new ReservationResponseDto
        {
            Id = reservation.Id,
            EquipmentId = reservation.EquipmentId,
            EquipmentName = reservation.Equipment?.Name ?? "Equipamento Oculto",
            UserId = reservation.UserId,
            UserName = reservation.User?.Name ?? "Utilizador Oculto",
            StartTime = reservation.ReservedForDate,
            EndTime = reservation.ExpirationDate,
            Status = "Cancelada"
        };
    }
}