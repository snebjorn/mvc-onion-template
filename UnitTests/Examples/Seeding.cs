using System;
using Core.DomainModel;
using Core.DomainServices;

namespace UnitTests.Examples
{
    public class Seeding
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Student> _studentRepository;
        private readonly IGenericRepository<Teacher> _teacherRepository;
        /*
         * Examples of functions that uses Faker.Net to generate data and 
         * seed a context with either departments or employees.
         * The functions uses a interface as argument, so either give your context this interface
         * or change the class, and adapt/change the functions to match the context you want to seed.
         */

        public Seeding(IUnitOfWork unitOfWork, IGenericRepository<Student> studentRepository, IGenericRepository<Teacher> teacherRepository)
        {
            _unitOfWork = unitOfWork;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
        }

        // Adds a student to the context.
        public void SeedStudent()
        {
            _studentRepository.Insert(CreateStudent());
            _unitOfWork.Save();
        }

        // Adds students to the context.
        public void SeedStudents(int amount)
        {
            for (var i = 0; i <= amount; i++) { 
                _studentRepository.Insert(CreateStudent());
            }
            _unitOfWork.Save();
        }

        // Adds a teacher to the context
        public void SeedTeacher()
        {
            _teacherRepository.Insert(CreateTeacher());
            _unitOfWork.Save();
        }

        // Adds teachers to the context.
        public void SeedTeachers(int amount)
        {
            for (var i = 0; i <= amount; i++)
            {
                _teacherRepository.Insert(CreateTeacher());
            }
            _unitOfWork.Save();
        }

        private Student CreateStudent()
        {
            return new Student()
            {
                Name = Faker.Company.Name(),
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            };
        }

        private Teacher CreateTeacher()
        {
            return new Teacher()
            {
                Name = Faker.Company.Name()
            };
        }
    }
}
