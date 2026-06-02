using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using EquipmentLoan.Application.Dtos.Login;
using EquipmentLoan.Application.Interfaces.Login;
using EquipmentLoan.Application.Interfaces.PasswordHasher;
using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Interfaces;
using EquipmentLoan.Exceptions;

namespace EquipmentLoan.Application.Services;

public class LoginUserService : ILoginUser
{
     private readonly IUserRepository _userRepository;
     private readonly IPasswordHasher _passwordHasher;

     public LoginUserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
     {
         _userRepository = userRepository;
         _passwordHasher = passwordHasher;
     }
     public async Task<LoginResponseDto> Execute(LoginRequestDto requestDto)
     {
         User user = await _userRepository.GetByEmailAsync(requestDto.Email);
         if (user == null)
             throw new BadHttpRequestException("E-mail ou senha incorretos.");
            
         var isPasswordValid = _passwordHasher.VerifyPassword(requestDto.Password, user.PasswordHash);
            
         if (!isPasswordValid)
             throw new BadHttpRequestException("E-mail ou senha incorretos.");

         var tokenHandler = new JwtSecurityTokenHandler();
            
         var key = Encoding.ASCII.GetBytes("SuaChaveSuperSecretaComPeloMenos32Caracteres!");

         var tokenDescriptor = new SecurityTokenDescriptor
         {
             Subject = new ClaimsIdentity(new[]
             {
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 new Claim(ClaimTypes.Name, user.Name),
                 new Claim(ClaimTypes.Email, user.Email),
                 new Claim(ClaimTypes.Role, user.Role.ToString()) 
             }),
             Expires = DateTime.UtcNow.AddHours(3), 
             SigningCredentials = new SigningCredentials(
                 new SymmetricSecurityKey(key), 
                 SecurityAlgorithms.HmacSha256Signature)
         };

         var token = tokenHandler.CreateToken(tokenDescriptor);

         return new LoginResponseDto
         {
             Token = tokenHandler.WriteToken(token),
             Name = user.Name,
             Email = user.Email,
             Role = user.Role.ToString()
         };
     }
}
    
