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
        private readonly string _studentName1 = Faker.Name.First();
        private readonly string _studentName2 = Faker.Name.First();

        /// <summary>
        /// Constructor for the test cases.
        /// For each test case, the class is instantiated and disposed.
        /// The basic naming of a test comprises of three main parts:
        /// [UnitOfWork_StateUnderTest_ExpectedBehavior]
        /// Followed by //Arange, //Act and //Assert
        /// </summary>
        public FakeDbContext()
        {
            // Example: https://msdn.microsoft.com/en-us/data/dn314429

            // Create fake context and populate it with some fake data. (Add more data as needed.)
            var substituteDbSet = Substitute.For<IDbSet<Student>>();
            var studentList = new List<Student>()
            {
                new Student() {Id = 0, Name = _studentName1 },
                new Student() {Id = 1, Name = _studentName2 }
            }.AsQueryable();

            // Setup for the substitute to behave like a DbSet.
            substituteDbSet.Provider.Returns(studentList.Provider);
            substituteDbSet.Expression.Returns(studentList.Expression);
            substituteDbSet.ElementType.Returns(studentList.ElementType);
            substituteDbSet.GetEnumerator().Returns(studentList.GetEnumerator());

            // Substitute the repository.
            _repo = Substitute.For<IGenericRepository<Student>>();

            // Set the AsQueryable to return the DbSet
            // You may need to set up other functions depending on the test case.
            _repo.AsQueryable().Returns(substituteDbSet.AsQueryable());

            // A substitute for the UnitOfWork, used when saving context.
            _unitOfWork = Substitute.For<IUnitOfWork>();
            
            // Pass the context to the controller, and use this for testing.
            _controller = new StudentController(_unitOfWork, _repo);

            // The controller uses automapper.
            Mapper.CreateMap<StudentViewModel, Student>().ReverseMap();
        }
        
        [Fact]
        public void IndexStudentsByName_CallingTheMethod_ReturnsStudents()
        {
            // Arange
            // Act
            var res = _controller.IndexStudentsByName();
            // Assert
            Assert.True(res.FindIndex(d => d.Name == _studentName1) > -1);
            Assert.True(res.FindIndex(d => d.Name == _studentName2) > -1);
        }
        
        [Fact]
        public void IndexStudentsById_CallingTheMethod_ReturnsStudents()
        {
            // Arange
            // Act
            var res = _controller.IndexStudentsById();
            // Assert
            Assert.True(res[0].Id == 0);
            Assert.True(res[1].Id == 1);
        }

        [Fact]
        public void NewStudent_CanCreateNewStudent_RepositoryReceivedInsertCallAndUnitOfWorkSaved()
        {
            // Arange
            var model = new StudentViewModel() { Name = Name.First() };
            // Act
            _controller.NewStudent(model);
            // Assert
            _repo.Received().Insert(Arg.Any<Student>());
            _unitOfWork.Received().Save();
        }

        [Fact]
        public void FindStudent_CanFindStudentWithId0_ReturnsANotNullObject()
        {
            // Arange
            var res = _controller.FindStudent(0);
            // Act
            Assert.NotNull(res);
            // Assert
            _repo.Received().AsQueryable();
        }
    }
}

