using EquipmentLoan.Application.DTOs;
using EquipmentLoan.Application.Interfaces;
using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Interfaces;

namespace EquipmentLoan.Application.Services;

public class GetEquipmentByIdService : IGetEquipmentById
{
    private readonly IEquipmentRepository _repository;
    public GetEquipmentByIdService(IEquipmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<EquipmentResponseDto> Execute(Guid id)
    {
        Equipment equipment = await _repository.GetByIdAsync(id);
        if (equipment == null) return null;

        return new EquipmentResponseDto
        {
            Id =  equipment.Id,
            Name = equipment.Name,
            SerialNumber =  equipment.SerialNumber,
            Status =   equipment.Status,    
            CategoryId =  equipment.CategoryId,
            CategoryName = equipment.Category.Name ?? "Sem Categoria",
        };
    }
}