using AutoMapper;
using Core.DomainModel;
using Web.Models;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Web.App_Start.MappingConfig), "Start")]

namespace Web.App_Start
{
    public class MappingConfig
    {
        public static void Start()
        {
            // ViewModel Mappings
            Mapper.CreateMap<StudentViewModel, Student>().ReverseMap();
        }
    }
}