using Back.Application.Models;
using Back.Application.Services.Interfaces;
using Back.Domain.Entities;
using CaseBack.Application.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;
        private readonly IConfiguration _configuration;
        private readonly IRecoveryEmailService recoveryEmailService;

        public UserController(IUserService service, IConfiguration _configuration, IRecoveryEmailService recoveryEmailService)
        {
            this.service = service;
            this._configuration = _configuration;
            this.recoveryEmailService = recoveryEmailService;

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOne(string id)
        {
            return new ResponseHelper().CreateResponse(await service.GetOne(id));
        }
        [HttpPost]
        [Route("add")]
        [AllowAnonymous]
        public async Task<IActionResult> Add(User user)
        {
            return new ResponseHelper().CreateResponse(await service.Add(user));
        }
        [HttpGet]
        [Route("todos")]
        public async Task<IActionResult> GetAll()
        {
            return new ResponseHelper().CreateResponse(await service.GetAll());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return new ResponseHelper().CreateResponse(await service.Delete(id));
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(string id, User user)
        {
            return new ResponseHelper().CreateResponse(await service.Update(id, user));
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTokenAsync([FromBody] User usr)
        {
            return new ResponseHelper().CreateResponse(await service.GetTokenAsync(usr));
        }

        [HttpPost]
        [Route("verify/{email}")]
        [AllowAnonymous]
        public async Task<IActionResult> SendEmail([FromRoute] string email)
        {
            return new ResponseHelper().CreateResponse(await recoveryEmailService.SendEmail(email));
        }

        [HttpPost]
        [Route("password")]
        [AllowAnonymous]
        public async Task<IActionResult> AlterPassword(AlterPasswordModel content)
        {
            return new ResponseHelper().CreateResponse(await recoveryEmailService.VerifyCode(content));
        }

        [HttpPost]
        [Route("alterPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> AlterPasswordAsync(AlterPasswordModel content)
        {
            return new ResponseHelper().CreateResponse(await service.AlterPasswordAsync(content));
        }
    }
}
