using EA_MVC_Practice.DTO;
using Microsoft.AspNetCore.Identity;

namespace EA_MVC_Practice.Interfaces
{
    public interface IAccountService
    {

        Task<IdentityResult> RegisterUserAsync(RegisterUserDTO request);

        Task LoginAsync(LoginDTO model);

        Task LogoutAsync();


    }
}
