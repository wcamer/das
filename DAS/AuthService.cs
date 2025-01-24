using DAS.Data;
using DAS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class AuthService
{
    private readonly DASContext _dbContext;
    private readonly IPasswordHasher<Profile> _PatientPasswordHasher;
    private readonly IPasswordHasher<ServiceProviders> _SPPasswordHasher;

    public AuthService(DASContext context, IPasswordHasher<Profile> PatientPasswordHasher, IPasswordHasher<ServiceProviders> SPPasswordHasher)
    {
        _dbContext = context;
        _PatientPasswordHasher = PatientPasswordHasher;
        _SPPasswordHasher = SPPasswordHasher;
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        var profile = await _dbContext.Profiles.FirstOrDefaultAsync(p => p.Email == email);
        if (profile == null)
        {
            var serviceProviderProfile = await _dbContext.ServiceProviders.FirstOrDefaultAsync(sp => sp.Email == email);
            if (serviceProviderProfile != null)
            {
                var sPResult = _SPPasswordHasher.VerifyHashedPassword(serviceProviderProfile, serviceProviderProfile.Password ?? "", password);
                if (sPResult == PasswordVerificationResult.Success)
                {
                    return new User
                    {
                        UserId = serviceProviderProfile.ServiceProvidersId,
                        Type = serviceProviderProfile.Type ?? "Non-User"
                    };
                }
                else
                {

                    Console.WriteLine($"--------------spresult----------{sPResult} has failedddddddd");
                    Console.WriteLine($"--------checking plaintext----------------");
                    var pTP = await _dbContext.ServiceProviders.FirstOrDefaultAsync(sp => sp.Email == email);
                    if (pTP != null)
                    {
                        return new User
                        {
                            UserId = serviceProviderProfile.ServiceProvidersId,
                            Type = serviceProviderProfile.Type ?? "Non-User"
                        };
                    }


                    return null;
                }
            }
            else
            {
                Console.WriteLine($"------------couldn't find a sp profile------------{serviceProviderProfile}-------");

                return null;
            }
        }
        else
        {
            // Verify password
            var result = _PatientPasswordHasher.VerifyHashedPassword(profile, profile.Password ?? "", password);
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
        // return null;


    }


}
