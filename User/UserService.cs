using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context){
        _context = context;
    }

    public async Task<User?> GetUserByIdAsync(Guid id){
        return await _context.Users.FindAsync(id);
    }

    public async Task<bool> UpdateUser(Guid id, UserUpdateRequest request)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        user.Idiom = request.Idiom;
        user.Username = request.Username;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUser(Guid id){
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return true;
    }
}

public class UserUpdateRequest
{
    public string Username { get; set; } = string.Empty;
    public string Idiom { get; set; } = string.Empty; 
}