using JWTAuthorization.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskWebApplication.Models;

namespace TaskWebApplication.Repository
{
    public class ValideToken
    {
        public bool validation { get; set; }

        public string? ErrorMessage { get; set; }
        public string? UserToken { get; set; }
    }
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IConfiguration configuration;

        public UserRepository(UserManager<AppUser> _userManager,SignInManager<AppUser> _signInManager,IConfiguration _configuration)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            configuration = _configuration;
        }

        public async Task<IdentityResult> UserRegister(UserRegisterDto userRegisterDto)
        {
            AppUser user = new AppUser()
            {
                Email = userRegisterDto.Email,
                UserName = userRegisterDto.Email
            };

            var result = await userManager.CreateAsync(user, userRegisterDto.Password);
            
            return result;
        }
        public async Task<ValideToken> UserLogin(UserLoginDto userLoginDto)
        {
            ValideToken result = new ValideToken();

            AppUser user = await userManager.FindByEmailAsync(userLoginDto.Email);

            if (user != null)
            {
                var idResult = await signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, false);
                if (idResult.Succeeded)
                {
                    idResult = await signInManager.PasswordSignInAsync(user, userLoginDto.Password, true, false);
                    result.validation = true;
                    result.UserToken = GenerateToken(userLoginDto);

                }
                else
                {
                    result.validation = false;
                    result.ErrorMessage = "Wrong Password";
                }

            }
            else
            {
                result.validation = false;
                result.ErrorMessage = "Wrong Email";
            }


            return result;
        }
        private string GenerateToken(UserLoginDto userLoginDto)
        {
            List<Claim> userClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,userLoginDto.Email)
                    };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:key"]));

            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenContrains = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.Today,
                issuer: configuration["jwt:issuser"],
                audience: configuration["jwt:issuser"],
                signingCredentials: signingCredentials
                );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            string token = handler.WriteToken(tokenContrains);

            return token;
        }
    }
}
