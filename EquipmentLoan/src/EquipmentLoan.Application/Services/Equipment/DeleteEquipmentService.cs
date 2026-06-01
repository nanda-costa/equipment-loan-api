using EquipmentLoan.Application.Interfaces;
using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Interfaces;

namespace EquipmentLoan.Application.Services
{
    public class DeleteEquipmentService : IDeleteEquipment
    {
        private readonly IEquipmentRepository _equipmentRepository;

        public DeleteEquipmentService(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        public async Task<bool> Execute(Guid id)
        {
            Equipment equipment = await _equipmentRepository.GetByIdAsync(id);
            if (equipment == null) return false;

            _equipmentRepository.Delete(equipment);
            return await _equipmentRepository.SaveChangesAsync();
        }
    }
}