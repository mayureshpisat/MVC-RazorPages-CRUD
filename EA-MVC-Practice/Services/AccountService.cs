using EA_MVC_Practice.Interfaces;
using Microsoft.AspNetCore.Identity;
using EA_MVC_Practice.DTO;

namespace EA_MVC_Practice.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IPasswordHasher<IdentityUser> _passwordHasher;

        public AccountService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager, IPasswordHasher<IdentityUser> passwordHasher) 
        { 
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
        }
        public async Task<IdentityResult> RegisterUserAsync(RegisterUserDTO request)
        {
            var user = new IdentityUser
            {
                Email = request.Email,
                UserName = request.Username,

            };

            var hashedPassword = _passwordHasher.HashPassword(user, request.Password);
            user.PasswordHash = hashedPassword;
            var result = await _userManager.CreateAsync(user);
            await _userManager.AddToRoleAsync(user, "User");
            return result;

        }

        public async Task LoginAsync(LoginDTO model)
        {

            var user = await _userManager.FindByNameAsync(model.Username);
            if(user == null)
            {
                throw new Exception("Unable to find user please check your username");
            }
            bool checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!checkPassword)
                throw new Exception("Invalid password");
            await _signInManager.SignInAsync(user, isPersistent: false);

        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }




    }

}
