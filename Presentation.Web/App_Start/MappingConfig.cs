using AutoMapper;
using Core.DomainModel;
using Presentation.Web.App_Start;
using Presentation.Web.Models;
using Presentation.Web.Models.Account;
using Presentation.Web.Models.Student;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MappingConfig), "Start")]

namespace Presentation.Web.App_Start
{
    /// <summary>
    /// Automapper is used to map attributes from your viewmodels to your domain models and vice versa,
    /// depending on the functionality. Using the same names for attributes is highly recommended, but
    /// sometimes more complex configrations are needed, which can also be configured here.
    /// </summary>
    public class MappingConfig
    {
        public static void Start()
        {
            // Write your AutoMapper configurations here.

            // ViewModel Mappings
            Mapper.CreateMap<NewStudentViewModel, Student>().ReverseMap();
            Mapper.CreateMap<RegisterViewModel, ApplicationUser>().ReverseMap();
        }
    }
}
