using System.ComponentModel.DataAnnotations;

namespace Presentation.Web.Models.Account
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
