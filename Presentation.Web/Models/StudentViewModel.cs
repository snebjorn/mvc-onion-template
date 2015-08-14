using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class StudentViewModel
    {
        [Display(Name = "Student Name:")]
        public string Name { get; set; }
    }
}