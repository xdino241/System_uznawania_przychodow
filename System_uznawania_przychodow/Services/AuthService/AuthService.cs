using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System_uznawania_przychodow.Data;
using System_uznawania_przychodow.DTOs.Auth;
using System_uznawania_przychodow.Exceptions;

namespace System_uznawania_przychodow.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly AppDbContext _dbContext;
    private readonly IConfiguration _config;
 
    public AuthService(AppDbContext dbContext, IConfiguration config)
    {
        _dbContext = dbContext;
        _config = config;
    }
 
    public async Task<TokenResponseDto> LoginAsync(LoginDto dto)
    {
        var employee = await _dbContext.Employees
            .FirstOrDefaultAsync(e => e.Login == dto.Login);
        if (employee == null)
        {
            throw new InvalidCredentialsException();
        }
        bool passwordValid = BCrypt.Net.BCrypt.Verify(dto.Password, employee.Password);
        if (!passwordValid)
        {
            throw new InvalidCredentialsException();
        }
 
        var token = GenerateToken(employee.EmployeeId, employee.Login, employee.Role);
        return new TokenResponseDto
        {
            AccessToken = token
        };
    }
 
    private string GenerateToken(int employeeId, string login, string role)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, employeeId.ToString()),
            new(ClaimTypes.Name, login),
            new(ClaimTypes.Role, role)
        };
 
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
 
        var jwt = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds);
 
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}