using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
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


        public ActionResult WindowsAuthenticationExample()
        {
            var test = WindowsIdentity.GetCurrent();

            // asdf@asf.dk
            // zZ@1234

            //var asdf = new WindowsTokenRoleProvider();
            //var roles = asdf.GetRolesForUser(test.Name);
            //HttpContext.GetOwinContext().ge

            ViewBag.Username = WindowsIdentity.GetCurrent().Name;
            ViewBag.Config = "I Web.config, <system.web> skift <authentication mode=\"None\" /> til <authentication mode=\"Windows\"> </authentication>";
            ViewBag.IIS = "IIS Serveren skal også sættes op til at tillade Windows Authentication.";
            return View();
        }
        
        [Authorize]
        public ActionResult AuthorizeAction()
        {
            ViewBag.Message = "Authorized";
            return View();
        }
    }
}