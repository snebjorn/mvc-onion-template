using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.DomainServices;
using Core.DomainModel;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGenericRepository<Student> _repo;

        public HomeController(IGenericRepository<Student> repo)
        {
            _repo = repo;
        }

        public ActionResult Index()
        {
            var a = _repo.Get();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}