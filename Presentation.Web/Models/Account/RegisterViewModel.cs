using System.ComponentModel.DataAnnotations;

namespace Presentation.Web.Models.Account
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
