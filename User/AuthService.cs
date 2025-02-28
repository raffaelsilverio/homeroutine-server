using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly JwtService _jwtService;

    public AuthService(AppDbContext context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<String?> Authenticate(string email, string password)
    {
        var user = await _context.Users.FindAsync(email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return null;
        return _jwtService.GenerateToken(user.Id, user.Email);
    }

    public async Task <String> Register(string username, string email, string password)
    {
        var user = new User { Username = username, Email = email, PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)  };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return _jwtService.GenerateToken(user.Id, user.Email);
    }
}