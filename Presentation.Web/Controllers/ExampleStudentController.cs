using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            return View();
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
         * Will return a json object which can be used in javascript
         */
        public ActionResult FindStudent(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var student = _studentRepository.AsQueryable().Single(x => x.Id == id);

            if (student == null)
                return HttpNotFound();

            var viewmodel = Mapper.Map<StudentViewModel>(student);

            return Json(viewmodel, JsonRequestBehavior.AllowGet);
        }
    }
}
