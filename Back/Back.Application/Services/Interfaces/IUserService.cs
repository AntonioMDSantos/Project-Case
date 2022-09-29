using Back.Application.Models;
using Back.Domain.Entities;
using CaseBack.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseModel> GetOne(string id);
        Task<ResponseModel> Add(User user);
        Task<ResponseModel> GetAll();
        Task<ResponseModel> Delete(string id);
        Task<ResponseModel> Update(string id, User user);
        Task<ResponseModel> GetTokenAsync(User usr);
        Task<ResponseModel> AlterPasswordAsync(AlterPasswordModel content);
    }
}
