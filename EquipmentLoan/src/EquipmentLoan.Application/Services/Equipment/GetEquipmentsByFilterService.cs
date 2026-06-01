using EquipmentLoan.Application.DTOs;
using EquipmentLoan.Application.Interfaces;
using EquipmentLoan.Domain.Enums;
using EquipmentLoan.Domain.Interfaces;

namespace EquipmentLoan.Application.Services
{
    public class GetEquipmentsByFilterService : IGetEquipmentsByFilter
    {
        private readonly IEquipmentRepository _equipmentRepository;

        public GetEquipmentsByFilterService(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        public async Task<IEnumerable<EquipmentResponseDto>> Execute(EquipmentStatus? status, Guid? categoryId)
        {
            var equipments = await _equipmentRepository.GetByFilterAsync(status, categoryId);

            return equipments.Select(e => new EquipmentResponseDto
            {
                Id = e.Id,
                Name = e.Name,
                SerialNumber = e.SerialNumber,
                Status = e.Status,
                CategoryId = e.CategoryId,
                CategoryName = e.Category?.Name ?? "Sem Categoria" 
            });
        }
    }
}