using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ManageIndexViewModelExample
    {
        public bool HasPassword { get; set; }
    }

    public class ChangePasswordViewModelExample
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}