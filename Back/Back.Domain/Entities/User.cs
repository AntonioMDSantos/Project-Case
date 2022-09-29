using Back.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back.Domain.Entities
{
    public class User : IdentityUser<string>
    {
        [NotMapped]
        public string Password { get; set; } = string.Empty;

        public eUserType Master { get; set; }

        public string? Image { get; set; }

    }
}
