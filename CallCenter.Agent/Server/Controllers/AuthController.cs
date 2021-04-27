using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CallCenter.Agent.Server.Auth.Models;
using CallCenter.Agent.Shared.Models;
using CallCenter.Agent.Server.Auth.Data;
using System;

namespace CallCenter.Agent.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDBContext _context;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDBContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return BadRequest("User does not exist");

            var singInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!singInResult.Succeeded)
                return BadRequest("Invalid password");

            await _signInManager.SignInAsync(user, request.RememberMe);
            return Ok();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest parameters)
        {
            var user = new ApplicationUser();
            user.UserName = parameters.UserName;
            var result = await _userManager.CreateAsync(user, parameters.Password);
            if (!result.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);

            return await Login(new LoginRequest
            {
                UserName = parameters.UserName,
                Password = parameters.Password
            });
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public CurrentUser CurrentUserInfo()
        {
            return new CurrentUser
            {
                IsAuthenticated = User.Identity.IsAuthenticated,
                UserName = User.Identity.Name,
                Claims = User.Claims
                .ToDictionary(c => c.Type, c => c.Value)
            };
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserById(string userId)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userId);

                if (user == null)
                {
                    return NotFound($"User not found with userId: '{userId}'.");
                }
                else
                {
                    var newUser = new ApplicationUser
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        EmailConfirmed = user.EmailConfirmed,
                        PhoneNumber = user.PhoneNumber
                    };

                    return Ok(newUser);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> AddUserRole([FromBody] UserRole userRole)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userRole.UserId);
                if (user == null)
                {
                    return NotFound($"User not found with userId: '{userRole.UserId}'.");
                }
                else
                {
                    string roleId = string.Empty;
                    var roleExist = _context.Roles.FirstOrDefault(x => x.Name.Equals(userRole.Role));

                    if (roleExist == null)
                    {
                        string id = Guid.NewGuid().ToString();
                        await _context.Roles.AddAsync(new IdentityRole { Id = id, Name = userRole.Role });
                        _context.SaveChanges();
                        roleId = id;
                    }
                    else
                    {
                        roleId = roleExist.Id;
                    }

                    var userRoleExist = from ur in _context.UserRoles
                                        where ur.UserId == user.Id && ur.RoleId == roleId
                                        select ur;

                    if (!userRoleExist.Any())
                    {
                        var userRoles = new IdentityUserRole<string>
                        {
                            UserId = user.Id,
                            RoleId = roleId
                        };
                        _context.UserRoles.Add(userRoles);
                        _context.SaveChanges();
                    }

                    return Ok($"{userRole.Role} role has been assign to user id {userRole.UserId}");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
