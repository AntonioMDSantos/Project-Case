using Back.Domain.Entities;
using Back.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly AppDbContext db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public HealthController(AppDbContext context, RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IConfiguration configuration)
        {
            db = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("createroles")]
        public async Task<ActionResult<string>> CreateRoles()
        {
            try
            {
                string[] roleNames = { "Master", "Common" };
                IdentityResult roleResult;
                foreach (var roleName in roleNames)
                {
                    var roleExist = await _roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                User poweruser = new User
                {
                    UserName = _configuration.GetSection("UserSettings")["UserEmail"],
                    Email = _configuration.GetSection("UserSettings")["UserEmail"],
                };

                string UserPassword = _configuration.GetSection("UserSettings")["UserPassword"];
                User? _user = null;

                _user = await _userManager.FindByEmailAsync(poweruser.Email);

                if (_user == null)
                {
                    var createPowerUser = await _userManager.CreateAsync(poweruser, UserPassword);
                    if (createPowerUser.Succeeded)
                    {
                        await _userManager.UpdateAsync(poweruser);
                        await _userManager.AddToRoleAsync(poweruser, "Master");
                    }
                }

                return Ok("Usuário padrão cadastrado");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "healthy zap";
        }
    }
}
