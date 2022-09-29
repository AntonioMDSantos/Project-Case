using Back.Application.Models;
using CaseBack.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Services.Interfaces
{
    public interface IRecoveryEmailService
    {
        Task<ResponseModel> SendEmail(string email);
        Task<ResponseModel> VerifyCode(AlterPasswordModel content);
    }
}
