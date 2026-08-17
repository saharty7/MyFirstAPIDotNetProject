using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using MyApi.DTOs;
using MyApi.Services;
using System.Collections.Generic;
using MyApi.Models;
using Microsoft.AspNetCore.Http;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var token = await _authService.Login(request);
            if(token == null)
                return Unauthorized("Invalid username or password."); 
            return Ok(new {token = token});
        }
    }
}
