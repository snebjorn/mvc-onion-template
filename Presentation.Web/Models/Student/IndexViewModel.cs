using System.ComponentModel.DataAnnotations;

namespace Presentation.Web.Models.Student
{
    public class IndexViewModel
    {
        [Display(Name = "Student Name:")]
        public string Name { get; set; }
        public int SelectedId { get; set; }
        public PagedData<NewStudentViewModel> PagedStudents { get; set; }
    }
}
