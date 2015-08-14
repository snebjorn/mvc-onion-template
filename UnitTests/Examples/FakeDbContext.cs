using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Faker;
using NSubstitute;
using Web.Controllers;
using Web.Models;
using Xunit;
using Core.DomainServices;
using Core.DomainModel;
using AutoMapper;

namespace UnitTests.Examples
{
    public class FakeDbContext
    {
        // http://romiller.com/2012/02/14/testing-with-a-fake-dbcontext/
        // For replacing a database with a fake DbContext.

        private readonly StudentController _controller;
        private readonly IGenericRepository<Student> _repo;
        private readonly IUnitOfWork _unitOfWork;

        // Usage of Faker.Net to generate random variables.
        private readonly string _studentName1 = Name.First();
        private readonly string _studentName2 = Name.First();
            
        public FakeDbContext()
        {
            // Example: https://msdn.microsoft.com/en-us/data/dn314429

            // Create fake context and populate it with some fake data.
            var substituteDbSet = Substitute.For<IDbSet<Student>>();
            var studentList = new List<Student>()
            {
                new Student() {Id = 0, Name = _studentName1 },
                new Student() {Id = 1, Name = _studentName2 }
            }.AsQueryable();

            // Setup for the substitute to behave like a DbSet that contains students.
            substituteDbSet.Provider.Returns(studentList.Provider);
            substituteDbSet.Expression.Returns(studentList.Expression);
            substituteDbSet.ElementType.Returns(studentList.ElementType);
            substituteDbSet.GetEnumerator().Returns(studentList.GetEnumerator());

            // A substitute for the repository.
            _repo = Substitute.For<IGenericRepository<Student>>();
            _repo.AsQueryable().Returns(substituteDbSet.AsQueryable());

            // A substitute for the UnitOfWork.
            _unitOfWork = Substitute.For<IUnitOfWork>();
            
            // Pass the context to the controller, and use this for testing.
            _controller = new StudentController(_unitOfWork, _repo);

            // The controller uses automapper.
            Mapper.CreateMap<StudentViewModel, Student>().ReverseMap();
        }

        [Fact]
        public void ContextStudentNameTest()
        {
            var res = _controller.IndexStudentsByName();

            Assert.True(res.FindIndex(d => d.Name == _studentName1) > -1);
            Assert.True(res.FindIndex(d => d.Name == _studentName2) > -1);
        }
        
        [Fact]
        public void ContextStudentIdTest()
        {
            var res = _controller.IndexStudentsById();

            Assert.True(res[0].Id == 0);
            Assert.True(res[1].Id == 1);
        }

        [Fact]
        public void TestNewStudent()
        {
            var model = new StudentViewModel() { Name = Name.First() };
            _controller.NewStudent(model);

            _repo.Received().Insert(Arg.Any<Student>());
            _unitOfWork.Received().Save();
        }
    }
}

