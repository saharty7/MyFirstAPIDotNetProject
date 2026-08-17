namespace MyApi.DTOs;

public class LoginRequest
{
    public string Username { set; get; } = string.Empty;
    public string Password { set; get; } = string.Empty;
}