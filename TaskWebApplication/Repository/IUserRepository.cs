using JWTAuthorization.Models;
using Microsoft.AspNetCore.Identity;

namespace TaskWebApplication.Repository
{
    public interface IUserRepository
    {
       Task<IdentityResult> UserRegister(UserRegisterDto userRegisterDto);
       Task<ValideToken> UserLogin(UserLoginDto userLoginDto);
    }
}
