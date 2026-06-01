using EquipmentLoan.Application.DTOs;
using EquipmentLoan.Application.Interfaces;
using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Enums;
using EquipmentLoan.Domain.Interfaces;

namespace EquipmentLoan.Application.Services
{
    public class CreateEquipmentService : ICreateEquipment
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CreateEquipmentService(IEquipmentRepository equipmentRepository, 
            ICategoryRepository categoryRepository)
        {
            _equipmentRepository = equipmentRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<EquipmentResponseDto> Execute(EquipmentCreateRequestDto requestDto)
        {
            Category category = await _categoryRepository.GetByIdAsync(requestDto.CategoryId);
            if (category == null)
            {
                throw new Exception("A categoria informada não existe."); // Futuramente tratada pelo Membro 4 em Exceptions
            }

            Equipment equipment = new Equipment
            {
                Name = requestDto.Name,
                SerialNumber = requestDto.SerialNumber,
                Status = EquipmentStatus.Available, // Todo equipamento novo entra como Disponível
                CategoryId = requestDto.CategoryId
            };
            
            await _equipmentRepository.AddAsync(equipment);
            await _equipmentRepository.SaveChangesAsync();

            return new EquipmentResponseDto
            {
                Id = equipment.Id,
                Name = equipment.Name,
                SerialNumber = equipment.SerialNumber,
                Status = equipment.Status,
                CategoryId = equipment.CategoryId,
                CategoryName = category.Name
            };
        }
    }
}