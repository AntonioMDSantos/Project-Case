using Back.Application.Models;
using Back.Application.Services.Interfaces;
using Back.Domain.Entities;
using Back.Persistence.Context;
using CaseBack.Application.Models;
using CaseBack.Application.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Back.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _UserManager;
        private readonly AppDbContext db;
        private readonly SignInManager<User> _signManager;
        public IConfiguration configuration { get; }
        public UserService(AppDbContext db, IConfiguration configuration, SignInManager<User> SignManager, UserManager<User> _UserManager)
        {
            this.db = db;
            this.configuration = configuration;
            this._signManager = SignManager;
            this._UserManager = _UserManager;
        }

        public async Task<ResponseModel> GetOne(string id)
        {
            try
            {
                var user = await db.Users.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (user == null)
                {
                    return new ResponseModel(404, "Usuário não encontrado");
                }

                return new ResponseModel(200, user);
            }
            catch (Exception e)
            {
                return new ResponseModel(500, e.Message);
            }
        }

        public async Task<ResponseModel> Add(User user)
        {
            try
            {
                var findUser = db.Users.Where(x => x.Email == user.Email).FirstOrDefault();

                if (findUser == null)
                {
                    var result = await _UserManager.CreateAsync(user, user.Password);

                    if (result.Succeeded)
                    {
                        return new ResponseModel(200, "Usuario criado com sucesso");
                    }
                    else
                    {
                        return new ResponseModel(406, "Informe os dados corretos");
                    }

                }

                return new ResponseModel(409, "Usuario já existe");
            }
            catch (Exception e)
            {
                return new ResponseModel(500, e.Message);
            }
        }

        public async Task<ResponseModel> GetAll()
        {
            try
            {
                var user = await db.Users.ToListAsync();

                if (user == null)
                {
                    return new ResponseModel(404, "Usuario não encontrado");
                }

                return new ResponseModel(200, user);
            }
            catch (Exception e)
            {
                return new ResponseModel(500, e.Message);
            }
        }

        public async Task<ResponseModel> Delete(string id)
        {
            try
            {
                var user = await db.Users.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (user == null)
                {
                    return new ResponseModel(404, "Usuario não encontrado");
                }
                await _UserManager.DeleteAsync(user);
                return new ResponseModel(200, user);
            }
            catch (Exception e)
            {
                return new ResponseModel(500, e.Message);
            }
        }
        public async Task<ResponseModel> Update(string id, User user)
        {
            try
            {
                var Finduser = await db.Users.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (Finduser == null)
                {
                    return new ResponseModel(404, "Usuario não encontrado");
                }
                if (!String.IsNullOrEmpty(user.Image) && !user.Image.Contains("Uploads/User"))
                {
                    Finduser.Image = user.Image != null ? MediaHelper.SaveMedia($"wwwroot/Uploads/User/{id}/Photo", user.Image) : Finduser.Image;
                }
                Finduser.UserName = user.UserName != "" ? user.UserName : Finduser.UserName;
                Finduser.Email = user.Email != "" ? user.Email : Finduser.Email;
                Finduser.Master = user.Master != null ? user.Master : Finduser.Master;
                Finduser.PasswordHash = user.Password != "" ? _UserManager.PasswordHasher.HashPassword(Finduser, user.Password) : Finduser.PasswordHash;
                var result = await _UserManager.UpdateAsync(Finduser);

                if (result.Succeeded)
                {
                    return new ResponseModel(200, "Usuário atualizado com sucesso", Finduser);
                }
                else
                {
                    return new ResponseModel(406, "Informe os dados corretos");
                }
            }
            catch (Exception e)
            {
                return new ResponseModel(500, e.Message);
            }
        }
        public SecurityToken GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("TokenAuthentication")["SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Authentication, user.Id)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = configuration.GetSection("TokenAuthentication")["Issuer"],
                Audience = configuration.GetSection("TokenAuthentication")["Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return token;
        }

        public async Task<ResponseModel> GetTokenAsync(User usr)
        {
            User? findUser = await db.Users.Where(x => x.Email == usr.Email).FirstOrDefaultAsync();

            if (findUser == null)
            {
                return new ResponseModel(401, "Usuário ou senha incorretos");
            }

            var result = await _signManager.PasswordSignInAsync(findUser, usr.Password, false, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                var defaultToken = GenerateToken(findUser);
                User? defaultRetUser;

                defaultRetUser = db.Users.Where(x => x.Id == findUser.Id)
                        .Select(x => new User()
                        {
                            Id = x.Id,
                            UserName = x.UserName,
                            Email = x.Email,
                            PhoneNumber = x.PhoneNumber,
                            Image = x.Image
                        })
                        .FirstOrDefault();

                return new ResponseModel(200, new GetTokenModel(
                    new JwtSecurityTokenHandler().WriteToken(defaultToken),
                    defaultRetUser,
                    defaultToken.ValidTo,
                    200)
                );
            }
            else
            {
                return new ResponseModel(401, "Usuário ou Senha incorretos");
            }
        }
        public async Task<ResponseModel> AlterPasswordAsync(AlterPasswordModel content)
        {
            try
            {
                var user = await db.Users.Where(x => x.Email == content.Email).FirstOrDefaultAsync();

                if (user != null)
                {
                    var result = await _signManager.PasswordSignInAsync(user, content.OldPassword, false, lockoutOnFailure: true);
                    if (result.Succeeded)
                    {
                        user.PasswordHash = _UserManager.PasswordHasher.HashPassword(user, content.NewPassword);
                        db.Users.Update(user);
                        await db.SaveChangesAsync();

                        return new ResponseModel(200, "Senha alterada com sucesso!");
                    }
                    return new ResponseModel(401, "Senha atual incorreta!");
                }
                return new ResponseModel(500, "Usuário não encontrado");
            }
            catch (Exception e)
            {
                return new ResponseModel(500, e.Message, "Erro no servidor!");
            }
        }
    }
}
