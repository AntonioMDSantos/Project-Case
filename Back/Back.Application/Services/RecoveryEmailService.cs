using Back.Application.Models;
using Back.Application.Services.Interfaces;
using Back.Domain.Entities;
using Back.Persistence.Context;
using CaseBack.Application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Back.Application.Services
{
    public class RecoveryEmailService : IRecoveryEmailService
    {
        private readonly AppDbContext db;
        private IConfiguration configuration { get; }
        private readonly UserManager<User> _UserManager;

        public RecoveryEmailService(AppDbContext db, IConfiguration configuration, UserManager<User> _UserManager)
        {
            this.db = db;
            this.configuration = configuration;
            this._UserManager = _UserManager;
        }
        public static Random rnd = new Random();
        public static string  codeRandom = rnd.Next(100000, 999999).ToString();
        string codigo = codeRandom;
        public async Task<ResponseModel> SendEmail(string email)
        {
            try
            {
                var user = await db.Users.Where(u => u.Email == email).FirstOrDefaultAsync();

                if (user != null)
                {
                    var apiKey = "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";
                    var client = new SendGridClient(apiKey);
                    var msg = new SendGridMessage();
                    msg.SetFrom(new EmailAddress("amedeiros@keener.io", "Keener"));
                    msg.AddTo(new EmailAddress(user.Email, user.UserName));
                    msg.SetTemplateId("d-eb7bb9de226f495289fefd731ab3d604");
                    msg.SetTemplateData(new
                    {
                        USER_NAME = user.UserName,
                        CODIGO = codigo
                    });
                    var response = await client.SendEmailAsync(msg);
                    return new ResponseModel(200, "Código enviado!", response);
                }

                return new ResponseModel(404, "Usuário não encontrado");
            }
            catch(Exception e)
            {
                return new ResponseModel(500, e.Message);
            }
        }

        public async Task<ResponseModel> VerifyCode(AlterPasswordModel content)
        {
            try
            {
                if (content.Code == codigo)
                {
                    var user = await db.Users.Where(u => u.Email == content.Email).FirstOrDefaultAsync();
                    user.PasswordHash = _UserManager.PasswordHasher.HashPassword(user, content.NewPassword);
                    db.Users.Update(user);
                    await db.SaveChangesAsync();

                    return new ResponseModel(200, "Senha alterada");
                }
                return new ResponseModel(401, "O codigo nao confere!");
            }
            catch(Exception e)
            {
                return new ResponseModel(500, e.Message);
            }
        }
    }
}     