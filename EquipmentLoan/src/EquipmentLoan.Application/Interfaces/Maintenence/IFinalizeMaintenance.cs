using EquipmentLoan.Application.Dtos.Maintenence;

namespace EquipmentLoan.Application.Interfaces.Maintenence;

public interface IFinalizeMaintenance
{
    public Task<MaintenanceResponseDto> Execute(MaintenanceFinalizeRequestDto requestDto);
}