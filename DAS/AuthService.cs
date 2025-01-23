using DAS.Data;
using DAS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class AuthService
{
    private readonly DASContext _dbContext;
    private readonly IPasswordHasher<Profile> _passwordHasher;

    public AuthService(DASContext context, IPasswordHasher<Profile> passwordHasher)
    {
        _dbContext = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        var profile = await _dbContext.Profiles.FirstOrDefaultAsync(p => p.Email == email);
        if (profile == null) return null;

        // Verify password
        var result = _passwordHasher.VerifyHashedPassword(profile, profile.Password ?? "", password);
        if (result == PasswordVerificationResult.Success)
        {
            return new User
            {
                UserId = profile.ProfileId,
                Type = profile.Type ?? "Non-user"


            };



        }

        return null;
    }


}
