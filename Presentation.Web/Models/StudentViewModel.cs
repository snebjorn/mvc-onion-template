using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Core.DomainModel;

namespace Web.Models
{
    public class IndexStudentViewModel
    {
        [Display(Name = "Student Name:")]
        public string Name { get; set; }
        public int SelectedId { get; set; }
        public PagedData<Student> PagedStudents { get; set; } 
    }

    public class StudentViewModel
    {
        public string Name { get; set; }
    }

    public class PagedData<T> where T : class
    {
        public IEnumerable<T> Data { get; set; }
        public int NumberOfPages { get; set; }
    }
}