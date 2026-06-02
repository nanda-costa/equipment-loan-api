using EquipmentLoan.Application.Dtos.Maintenence;
using EquipmentLoan.Application.Interfaces.Maintenence;
using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Enums;
using EquipmentLoan.Domain.Exceptions;
using EquipmentLoan.Domain.Interfaces;

namespace EquipmentLoan.Application.Services
{
    public class StartMaintenanceService : IStartMaintenance
    {
        private readonly IMaintenanceRepository _maintenanceRepository;
        private readonly IEquipmentRepository _equipmentRepository;

        public StartMaintenanceService(
            IMaintenanceRepository maintenanceRepository,
            IEquipmentRepository equipmentRepository)
        {
            _maintenanceRepository = maintenanceRepository;
            _equipmentRepository = equipmentRepository;
        }

        public async Task<MaintenanceResponseDto> Execute(MaintenanceCreateRequestDto requestDto)
        {
            Equipment equipment = await _equipmentRepository.GetByIdAsync(requestDto.EquipmentId);
            if (equipment == null) throw new Exception("Equipamento não encontrado.");

            if (equipment.Status == EquipmentStatus.Maintenance)
                throw new BusinessException("Este equipamento já se encontra em processo de manutenção.");

            equipment.Status = EquipmentStatus.Maintenance;
            _equipmentRepository.Update(equipment);

            Maintenance maintenance = new Maintenance
            {
                EquipmentId = requestDto.EquipmentId,
                Description = requestDto.Description,
                StartDate = DateTime.UtcNow,
                EndDate = null 
            };

            await _maintenanceRepository.AddAsync(maintenance);
            
            await _maintenanceRepository.SaveChangesAsync();
            await _equipmentRepository.SaveChangesAsync();

            return new MaintenanceResponseDto
            {
                Id = maintenance.Id,
                EquipmentId = maintenance.EquipmentId,
                EquipmentName = equipment.Name,
                Description = maintenance.Description,
                StartDate = maintenance.StartDate,
                EndDate = maintenance.EndDate,
                IsResolved = false
            };
        }
    }
}