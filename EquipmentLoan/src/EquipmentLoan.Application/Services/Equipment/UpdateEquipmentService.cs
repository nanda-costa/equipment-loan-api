using EquipmentLoan.Application.DTOs;
using EquipmentLoan.Application.Interfaces;
using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Interfaces;

namespace EquipmentLoan.Application.Services
{
    public class UpdateEquipmentService : IUpdateEquipment
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly ICategoryRepository _categoryRepository;

        public UpdateEquipmentService(IEquipmentRepository equipmentRepository, ICategoryRepository categoryRepository)
        {
            _equipmentRepository = equipmentRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Execute(Guid id, EquipmentCreateRequestDto requestDto)
        {
            Equipment equipment = await _equipmentRepository.GetByIdAsync(id);
            if (equipment == null) return false;

            Category categoryExists = await _categoryRepository.GetByIdAsync(requestDto.CategoryId);
            if (categoryExists == null) throw new Exception("A categoria informada não existe.");

            equipment.Name = requestDto.Name;
            equipment.SerialNumber = requestDto.SerialNumber;
            equipment.CategoryId = requestDto.CategoryId;

            _equipmentRepository.Update(equipment);
            return await _equipmentRepository.SaveChangesAsync();
        }
    }
}