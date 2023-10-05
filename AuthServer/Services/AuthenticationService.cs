using AuthServer.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthServer.Services
{
    public class AuthenticationService
    {
        UserManager<IdentityUser> _UserManager;
        SignInManager<IdentityUser> _SignInManger;
        public AuthenticationService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManger)
        {
            _UserManager = userManager;
            _SignInManger = signInManger;
        }
        public async Task<bool> RegisterNewUser(RegisterUser user)
        {
            var identityUser = new IdentityUser()
            {
                Email = user.Email,
                UserName = user.Email
            };
            var result = await _UserManager.CreateAsync(identityUser, user.Password);
            if (result.Succeeded) 
            {
                return true;
            }
            foreach (var err in result.Errors)
            {
                Console.WriteLine(err.Code);
            }
            return false;
        }
        public async Task<bool > Authenticate(LoginUser user)
        {
            var result = await _SignInManger.PasswordSignInAsync(user.Email, user.Password, false, true);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}
