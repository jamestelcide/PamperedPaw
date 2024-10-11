using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PamperedPaw.Core.Domain.IdentityEntities;
using PamperedPaw.Core.DTO;

namespace PamperedPaw.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager, 
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApplicationUser>> PostRegister(RegisterDto registerDto)
        {

        }

        [HttpGet]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {

        }

        [HttpPost("login")]
        public async Task<IActionResult> PostLogin(LoginDto loginDto)
        {

        }

        [HttpGet("logout")]
        public async Task<IActionResult> GetLogout()
        {

        }
    }
}
