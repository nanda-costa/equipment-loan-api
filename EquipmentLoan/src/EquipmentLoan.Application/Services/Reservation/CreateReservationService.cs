using EquipmentLoan.Application.Dtos.Reservation;
using EquipmentLoan.Application.Interfaces.Reservation;
using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Interfaces;

namespace EquipmentLoan.Application.Services
{
    public class CreateReservationService : ICreateReservation
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IUserRepository _userRepository;

        public CreateReservationService(
            IReservationRepository reservationRepository,
            IEquipmentRepository equipmentRepository,
            IUserRepository userRepository)
        {
            _reservationRepository = reservationRepository;
            _equipmentRepository = equipmentRepository;
            _userRepository = userRepository;
        }

        public async Task<ReservationResponseDto> Execute(ReservationCreateRequestDto requestDto)
        {
            if (requestDto.StartTime < DateTime.UtcNow)
                throw new Exception("A data de início da reserva não pode ser no passado.");

            if (requestDto.EndTime <= requestDto.StartTime)
                throw new Exception("A data de fim deve ser posterior à data de início.");

            var equipment = await _equipmentRepository.GetByIdAsync(requestDto.EquipmentId);
            if (equipment == null) throw new Exception("Equipamento não encontrado.");

            var user = await _userRepository.GetByIdAsync(requestDto.UserId);
            if (user == null) throw new Exception("Utilizador não encontrado.");

            var isOverlapping = await _reservationRepository.HasOverlappingReservationAsync(
                requestDto.EquipmentId, requestDto.StartTime, requestDto.EndTime);

            if (isOverlapping)
                throw new Exception("Este equipamento já possui uma reserva ativa para o período selecionado.");

            // 🛠️ MAPEAMENTO CORRIGIDO COM OS NOMES DA SUA ENTIDADE:
            var reservation = new Reservation
            {
                EquipmentId = requestDto.EquipmentId,
                UserId = requestDto.UserId,
                ReservedForDate = requestDto.StartTime, // Mudado aqui
                ExpirationDate = requestDto.EndTime,   // Mudado aqui
                IsActive = true
            };

            await _reservationRepository.AddAsync(reservation);
            await _reservationRepository.SaveChangesAsync();

            return new ReservationResponseDto
            {
                Id = reservation.Id,
                EquipmentId = reservation.EquipmentId,
                EquipmentName = equipment.Name,
                UserId = reservation.UserId,
                UserName = user.Name,
                StartTime = reservation.ReservedForDate, // Mudado aqui
                EndTime = reservation.ExpirationDate,    // Mudado aqui
                Status = "Ativa"
            };
        }
    }
}