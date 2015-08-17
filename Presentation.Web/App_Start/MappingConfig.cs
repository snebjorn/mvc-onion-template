using AutoMapper;
using Core.DomainModel;
using Web.Models;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Web.MappingConfig), "Start")]

namespace Web
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