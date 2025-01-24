using DAS.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

public class UserService
{
    private readonly ProtectedLocalStorage _PLS;

    public UserService(ProtectedLocalStorage PLS)
    {
        _PLS = PLS;
    }

    private string key = "CurrentUser";

    //this will be the object that will be stored in ProtectedLocalStorage
    public User? CurrentUser { get; set; }

    //This will set the given user object in PLS by the key name "CurrentUser" 
    public async Task SetLoggedInUser(User user)
    {
        await _PLS.DeleteAsync(key);
        await _PLS.SetAsync(key, user);
    }

    //This will return the user object stored in PLS by the key name "CurrentUser"
    public async Task<User?> GetLoggedInUser()
    {
        var result = await _PLS.GetAsync<User>("CurrentUser");
        if (result.Success)
        {
            // Console.WriteLine($"*****************result.Value:{result.Value.UserId}");

            return result.Value;
        }
        else
        {
            return null;
        }


    }

    //This will delete the user object in PLS by the key name "CurrentUser"
    public async Task DeleteLoggedInUser()
    {
        await _PLS.DeleteAsync(key);
    }


}
