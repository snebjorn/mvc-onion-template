using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class IndexStudentViewModel
    {
        [Display(Name = "Student Name:")]
        public string Name { get; set; }
        public int SelectedId { get; set; }
        public PagedData<StudentViewModel> PagedStudents { get; set; } 
    }

    public class StudentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MailViewModel
    {
        public string Subject { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
    }
}
