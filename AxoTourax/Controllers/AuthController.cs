using AxoTourax.Configuration;
using AxoTourax.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AxoTourax.Controllers
{
    [Route("api/[controller]")] // api/auth
    [ApiController]
    [AllowAnonymous]
    // [Authorize(Roles = "Admin")]
    // [Authorize]   // just require any authentication
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtConfig _jwtConfig;

        public AuthController(
            UserManager<IdentityUser> userManager ,
            IOptionsMonitor<JwtConfig> optionsMonitor,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);

            if (existingUser != null) return BadRequest("Email already used");

            var newUser = new IdentityUser { Email = user.Email, UserName = user.Email };
            var isCreated = await _userManager.CreateAsync(newUser , user.Password);

            if (isCreated.Succeeded) return Ok(await GenerateJwtTokenAsync(newUser));

            return BadRequest($"An error happened while creating your account : {isCreated.GetErrors()}");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(User user)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);

            if (existingUser == null) return BadRequest("Can't find account with this email");

            bool isCorrect = await _userManager.CheckPasswordAsync(existingUser , user.Password);

            if (!isCorrect) return BadRequest("Invalid password");

            return Ok(await GenerateJwtTokenAsync(existingUser));
        }

        private async Task<string> GenerateJwtTokenAsync(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key) , SecurityAlgorithms.HmacSha256Signature)
            };

            var claims = new List<Claim>
            {
                new Claim("Id" , user.Id) ,
                new Claim(JwtRegisteredClaimNames.Email , user.Email)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var roleName in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleName));
            }

            tokenDescriptor.Subject = new ClaimsIdentity(claims);

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

        private async Task CreateRolesAsync()
        {
            var roles = new List<IdentityRole>
            {
                new() {Name = "Anonymous"},
                new() {Name = "Contributor"},
                new() {Name = "Admin"},
            };

            foreach(var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role.Name))
                    await _roleManager.CreateAsync(role);
            }
        }
    }
}
