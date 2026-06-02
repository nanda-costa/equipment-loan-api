using EquipmentLoan.Application.Dtos.Maintenence;
using EquipmentLoan.Application.Interfaces.Maintenence;
using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Enums;
using EquipmentLoan.Domain.Interfaces;

namespace EquipmentLoan.Application.Services;

public class FinalizeMaintenanceService : IFinalizeMaintenance
{
    private readonly IMaintenanceRepository _maintenanceRepository;
    private readonly IEquipmentRepository _equipmentRepository;

    public FinalizeMaintenanceService(
        IMaintenanceRepository maintenanceRepository,
        IEquipmentRepository equipmentRepository)
    {
        _maintenanceRepository = maintenanceRepository;
        _equipmentRepository = equipmentRepository;
    }

    public async Task<MaintenanceResponseDto> Execute(MaintenanceFinalizeRequestDto requestDto)
    {
        Maintenance maintenance = await _maintenanceRepository.GetByIdAsync(requestDto.MaintenanceId);
        if (maintenance == null) 
            throw new Exception("Registro de manutenção não encontrado.");

        if (maintenance.EndDate != null)
            throw new Exception("Esta manutenção já foi finalizada anteriormente.");

        Equipment equipment = await _equipmentRepository.GetByIdAsync(maintenance.EquipmentId);
        if (equipment == null) 
            throw new Exception("Equipamento vinculado a esta manutenção não foi encontrado.");

        maintenance.EndDate = DateTime.UtcNow;
        _maintenanceRepository.Update(maintenance);

        equipment.Status = EquipmentStatus.Available;
        _equipmentRepository.Update(equipment);

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
            IsResolved = true
        };
    }
}