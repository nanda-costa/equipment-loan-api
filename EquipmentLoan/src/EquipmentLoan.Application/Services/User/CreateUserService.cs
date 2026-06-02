using EquipmentLoan.Application.DTOs;
using EquipmentLoan.Application.Interfaces.PasswordHasher;
using EquipmentLoan.Application.Interfaces.User;
using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Interfaces;
using EquipmentLoan.Exceptions;

namespace EquipmentLoan.Application.Services;

public class CreateUserService : ICreateUser
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserResponseDto> Execute(UserCreateRequestDto requestDto)
    {
        User existingUser = await _userRepository.GetByEmailAsync(requestDto.Email);
        if (existingUser != null)
        {
            throw new BadHttpRequestException("Este e-mail já está em uso no sistema.");
        }

        User user = new User
        {
            Name = requestDto.Name,
            Email = requestDto.Email,
            PasswordHash = _passwordHasher.HashPassword(requestDto.Password),
            Role = requestDto.Role
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };
    }
}