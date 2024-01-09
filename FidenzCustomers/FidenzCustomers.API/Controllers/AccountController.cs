using Asp.Versioning;
using FidenzCustomers.API.Common.Utility;
using FidenzCustomers.Application.DTOs;
using FidenzCustomers.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FidenzCustomers.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IConfiguration _config;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] UserDto user)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        var userDB = _userManager.FindByEmailAsync(user.Email).Result;
                        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Email, userDB.Email),
                            new Claim(JwtRegisteredClaimNames.Sub, userDB.UserName),
                            new Claim(SD.Role_Admin, _userManager.IsInRoleAsync(userDB, SD.Role_Admin).Result ? "true" : "false",ClaimValueTypes.Boolean),
                        };

                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(claims),
                            Expires = DateTime.Now.AddMinutes(30),
                            SigningCredentials = credentials,
                            Issuer = _config["Jwt:Issuer"],
                            Audience = _config["Jwt:Audience"]
                        };

                        var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
                        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                        return Ok(new { token = tokenString });
                    }
                    else
                    {
                        return BadRequest("Invalid Credentials");
                    }
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] UserRegisterationDto registerUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = new()
                    {
                        UserName = registerUser.Email,
                        Email = registerUser.Email,
                        Name = registerUser.Name,
                        CreatedAt = DateTime.Now,
                    };
                    var result = await _userManager.CreateAsync(user, registerUser.Password);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(registerUser.Role))
                        {
                            await _userManager.AddToRoleAsync(user, registerUser.Role);
                        }
                        else
                        {
                            await _userManager.AddToRoleAsync(user, SD.Role_Customer);
                        }
                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        return BadRequest(error.Description);
                    }

                }
                else
                {
                    return BadRequest(ModelState);
                }
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult GetUser()
        {
            try
            {
                var accessToken = Request.Headers[HeaderNames.Authorization];
                if (accessToken.Count == 0)
                {
                    return BadRequest("Invalid Token");
                }
                var token = accessToken.ToString().Split(" ")[1];
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token);
                var tokenS = handler.ReadToken(token) as JwtSecurityToken;
                var email = tokenS.Claims.First(claim => claim.Type == "email").Value;
                var user = _userManager.FindByEmailAsync(email).Result;
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


    }
}
