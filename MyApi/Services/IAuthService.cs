using Microsoft.AspNetCore.Identity.Data;
using MyApi.Models;
namespace MyApi.Services;

public interface IAuthService
{
    Task<string?> Login(MyApi.DTOs.LoginRequest request); 
}