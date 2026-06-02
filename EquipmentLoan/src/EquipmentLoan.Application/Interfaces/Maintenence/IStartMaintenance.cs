using EquipmentLoan.Application.Dtos.Maintenence;

namespace EquipmentLoan.Application.Interfaces.Maintenence;

public interface IStartMaintenance
{
    public Task<MaintenanceResponseDto> Execute(MaintenanceCreateRequestDto requestDto);
}