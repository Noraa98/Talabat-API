using LinkDev.Talabat.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Application.Exceptions;
using LinkDev.Talabat.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace LinkDev.Talabat.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager ,IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        // login
        public async Task<UserDto> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                throw new UnAuthorizeException("Invalid Login");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);
            
            if (result.IsNotAllowed) 
                throw new UnAuthorizeException("You are not allowed to login");
            if (result.IsLockedOut) 
                throw new UnAuthorizeException("Your account is locked out");
            if (!result.Succeeded)
                throw new UnAuthorizeException("Invalid Login");

            var response = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await GenerateTokenAsync(user)
            };

            return response;

        }

        // register
        public async Task<UserDto> RegisterAsync(RegisterDto model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
                throw new BadRequestException("Email is already registered");

            var user = new ApplicationUser
            {
                DisplayName = model.DisplayName,
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumder
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BadRequestException($"User registration failed: {errors}");
            }
            var response = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await GenerateTokenAsync(user)
            };
            return response;
        }

        // create Token
        private async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            // 1) Get Claims from DB
            var userClaims = await _userManager.GetClaimsAsync(user);

            // 2) Roles (strings)
            var userRoles = await _userManager.GetRolesAsync(user);

            // 3) Convert Roles to Claims
            var rolesClaims = userRoles.Select(role => new Claim(ClaimTypes.Role, role));

            // 4) Build Claims List
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.DisplayName)
            }
            .Union(userClaims)
            .Union(rolesClaims);

            // Read JWT settings from appsettings.json
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Generate Token Object
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWT:DurationInMinutes"]!)),
                claims: claims,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
