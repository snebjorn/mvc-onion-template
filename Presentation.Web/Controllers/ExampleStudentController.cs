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
            var student = Mapper.Map<Student>(model);
            student.CreatedOn = DateTime.Now;
            student.ModifiedOn = DateTime.Now;
            _studentRepository.Insert(student);

            _unitOfWork.Save();
            return View(model);
        }

        public List<Student> IndexStudentsById()
        {
            return _studentRepository.AsQueryable().OrderBy(d => d.Id).ToList();
        }

        public List<Student> IndexStudentsByName()
        {
            return _studentRepository.AsQueryable().OrderBy(e => e.Name).ToList();
        }

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
