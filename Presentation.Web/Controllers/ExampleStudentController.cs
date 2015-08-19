using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Core.DomainModel;
using Core.DomainServices;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IGenericRepository<Student> _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        // Hardcoded pagingsize
        private const int PageSize = 3;

        /* The constructor takes GenericRepositories as argument, which is automatically created by ninject in NinjectWebCommon.
         * This enables us to quickly get references to our context, by simply typing, for 
         * example, "IGenericRepository<Course> courseRepository", which we can use right away.
         * Whenever a changes is made, use _unitOfWork.Save() to save any changes.
         */
        public StudentController(IUnitOfWork unitOfWork, IGenericRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Students are here.";

            // We're putting the SelectListItems into a ViewBag, because we do not need to send it back.
            // We only send the selectedId back.
            ViewBag.StudentIds = _studentRepository.AsQueryable().Select(s =>
                new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = s.Id.ToString()
                });

            // Example of a dropdown menu.
            var model = new IndexStudentViewModel()
            {
                PagedStudents = 
                    new PagedData<Student>()
                    {
                        Data = _studentRepository.AsQueryable().OrderBy(p => p.Name).Take(PageSize).ToList(),
                        NumberOfPages = PageingsSizeHelper()
                    }
            };
            return View(model);
        }

        private int PageingsSizeHelper()
        {
            return Convert.ToInt32(Math.Ceiling((double) _studentRepository.Count()/PageSize));
        }

        public ActionResult _Students(int page)
        {
            var model = new IndexStudentViewModel()
            {
                PagedStudents = new PagedData<Student>()
                {
                    Data = _studentRepository.AsQueryable().OrderBy(p => p.Name).Skip(PageSize * (page -1)).Take(PageSize).ToList(),
                    NumberOfPages = PageingsSizeHelper()
                }
            };
            return PartialView(model);
        }

        public ActionResult NewStudent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewStudent(StudentViewModel model)
        {
            // Configure automapper into mapping viewmodels against domainmodels.
            // This can also be done conversely.
            var student = Mapper.Map<Student>(model);
            student.CreatedOn = DateTime.Now;
            student.ModifiedOn = DateTime.Now;
            _studentRepository.Insert(student);

            _unitOfWork.Save();
            return View(model);
        }

        // Functions used in UnitTest Examples - How to test a controller (FakeDbContext).
        public List<Student> IndexStudentsById()
        {
            return _studentRepository.AsQueryable().OrderBy(d => d.Id).ToList();
        }

        public List<Student> IndexStudentsByName()
        {
            return _studentRepository.AsQueryable().OrderBy(e => e.Name).ToList();
        }

        /* Function used to find a student by id.
         * Will return a json object which can be used in javascript.
         */
        public ActionResult FindStudent(int? id)
        {
            if (id == null)
                return Json(new StudentViewModel(){ Name = "null" }, JsonRequestBehavior.AllowGet);

            var student = _studentRepository.AsQueryable().Single(x => x.Id == id);

            if (student == null)
                return HttpNotFound();

            var viewmodel = Mapper.Map<StudentViewModel>(student);

            return Json(viewmodel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult StudentPaging()
        {
            // Construct a PagedData class.
            var pagedStudents = new PagedData<Student>();
            _studentRepository.AsQueryable();
            return Json(pagedStudents, JsonRequestBehavior.AllowGet);
        }
    }
}
