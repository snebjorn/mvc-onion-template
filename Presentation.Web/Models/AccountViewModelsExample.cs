using System.ComponentModel.DataAnnotations;

namespace Presentation.Web.Models
{
    public class RegisterViewModelExample
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModelExample
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class ForgotPasswordViewModelExample
    {
        public string Email { get; set; }
    }
}
