using EquipmentLoan.Application.DTOs;
using EquipmentLoan.Application.Interfaces.User;
using EquipmentLoan.Domain.Interfaces;

namespace EquipmentLoan.Application.Services
{
    public class GetAllUsersService : IGetAllUsers
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserResponseDto>> Execute()
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role
            });
        }
    }
}