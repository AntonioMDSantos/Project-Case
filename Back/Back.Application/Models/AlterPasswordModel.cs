using CaseBack.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Models
{
    public class AlterPasswordModel
    {
        public string? NewPassword { get; set; }
        public string? Code { get; set; }
        public string? Email { get; set; }
        public string? OldPassword {get; set;}

        public AlterPasswordModel(string code, string newPassword, string email, string oldPassword)
        {
            this.Code = code;
            this.NewPassword = newPassword;
            this.Email = email;
            this.OldPassword = oldPassword;
        }
    }

}