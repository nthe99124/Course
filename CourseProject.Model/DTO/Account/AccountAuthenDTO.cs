using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace CourseProject.Model.DTO.Accountant
{
    public class AccountAuthenDTO
    {
        public ClaimsIdentity ClaimsIdentity { get; set; }
        public AuthenticationProperties AuthProperties { get; set; }
        public string UserName { get; set; }
        public long Coin { get; set; }
    }
}
