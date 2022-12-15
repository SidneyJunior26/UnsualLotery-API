using System;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace UnsualLotery.Services.Users;

public class UserCreatorService {

    private readonly UserManager<IdentityUser> _userManager;

    public UserCreatorService(UserManager<IdentityUser> userManager) {
        _userManager = userManager;
    }

    public async Task<(IdentityResult, string)> Create(string email, string password, string phoneNumber, List<Claim> claims) {

        var newUser = new IdentityUser { UserName = email, Email = email, PhoneNumber = phoneNumber };
        var result = await _userManager.CreateAsync(newUser, password);

        if (!result.Succeeded)
            return (result, String.Empty);

        return (await _userManager.AddClaimsAsync(newUser, claims), newUser.Id);
    }
}