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
        private readonly IGenericRepository<Test> _repo;

        public HomeController(IGenericRepository<Test> repo)
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

        public void FromParameter(string name, int age)
        {
            var person = new PersonViewModel 
            { 
                Name = name, Age = age 
            };
        }

        public void FromFormCollection(FormCollection form)
        {
            var person = new PersonViewModel
            {
                Name = form["name"], Age = int.Parse(form["age"])
            };
        }

        public void FromRequest()
        {
            var person = new PersonViewModel
            {
                Name = Request.Form["name"],
                Age = int.Parse(Request.Form["age"])
            };
        }

        public void FromComplexType(PersonViewModel model)
        {
            var person = new PersonViewModel
            {
                Name = model.Name, Age = model.Age
            };
        }
    }

    public class PersonViewModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}