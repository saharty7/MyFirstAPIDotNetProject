using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MyApi.DTOs;
using MyApi.Services;
using MyApi.Data;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    public AuthService (ApplicationDbContext context, IConfiguration configuration)
    {
        _configuration = configuration;
        _context = context;
    }

    public async Task<string?> Login(MyApi.DTOs.LoginRequest request)
    {
        var staff = await _context.Staff.FirstOrDefaultAsync(s => s.Username == request.Username);
        if (staff == null)
            return null;
        bool passwordValid = BCrypt.Net.BCrypt.Verify(request.Password, staff.Password);
        if (!passwordValid)
            return null;
        var claims = new []
        {
            new Claim(ClaimTypes.Name, staff.Username),
            new Claim(ClaimTypes.NameIdentifier, staff.Id.ToString())
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"], 
        audience:_configuration["Jwt:Audience"],
        claims:claims,
        expires:DateTime.UtcNow.AddHours(2),
        signingCredentials:credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}