using AutoMapper;
using Core.DomainModel;
using Presentation.Web.Models.Account;
using Presentation.Web.Models.Student;

namespace Presentation.Web.App_Start
{
    /// <summary>
    /// Automapper mapping profile.
    /// It's used to map properties from your domain models to your view models and vice versa.
    /// Using the same name for properties in both your domain and view models is highly recommended,
    /// as this will allow for convention-based mapping without any setup.
    /// Sometimes more complex configrations are needed, which can also be configured here.
    /// AutoMapper configuration wiki: https://github.com/AutoMapper/AutoMapper/wiki/Configuration
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, NewStudentViewModel>().ReverseMap();
            CreateMap<ApplicationUser, RegisterViewModel>().ReverseMap();
        }
    }
}
