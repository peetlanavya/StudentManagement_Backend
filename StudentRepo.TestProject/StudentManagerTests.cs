using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using StudentRepo.Data;
using StudentRepo.Models;
using StudentRepo.Services;
using System;

namespace Testing1.Tests
{
    [TestClass]
    public class StudentManagerTests
    {
        private StudentManager _manager;
        private StudentDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<StudentDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new StudentDbContext(options);
            _manager = new StudentManager(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context?.Dispose();
        }

        private void AssertThrows<TException>(Action action) where TException : Exception
        {
            try
            {
                action();
                Assert.Fail($"Expected {typeof(TException).Name} but no exception was thrown");
            }
            catch (TException)
            {
                // Expected
            }
            catch (Exception ex)
            {
                Assert.Fail($"Expected {typeof(TException).Name} but got {ex.GetType().Name}: {ex.Message}");
            }
        }

        [TestMethod]
        public void AddStudent_NullStudent_ThrowsException()
        {
            AssertThrows<CustomException>(() => _manager.AddStudent(null));
        }

        [TestMethod]
        public void AddStudent_EmptyName_ThrowsException()
        {
            AssertThrows<CustomException>(() => 
                _manager.AddStudent(new Student { Name = "", Age = 20, Email = "a@a.com", PhoneNumber = "1" }));
        }

        [TestMethod]
        public void AddStudent_InvalidAge_ThrowsException()
        {
            AssertThrows<CustomException>(() => 
                _manager.AddStudent(new Student { Name = "John", Age = 0, Email = "a@a.com", PhoneNumber = "1" }));
        }

        [TestMethod]
        public void AddStudent_EmptyEmail_ThrowsException()
        {
            AssertThrows<CustomException>(() => 
                _manager.AddStudent(new Student { Name = "John", Age = 25, Email = "", PhoneNumber = "1234567890" }));
        }

        [TestMethod]
        public void AddStudent_EmptyPhoneNumber_ThrowsException()
        {
            AssertThrows<CustomException>(() => 
                _manager.AddStudent(new Student { Name = "John", Age = 25, Email = "john@example.com", PhoneNumber = "" }));
        }

        [TestMethod]
        public void UpdateStudent_NullStudent_ThrowsException()
        {
            AssertThrows<CustomException>(() => _manager.UpdateStudent(null));
        }

        [TestMethod]
        public void UpdateStudent_InvalidId_ThrowsException()
        {
            AssertThrows<CustomException>(() => 
                _manager.UpdateStudent(new Student { Id = 0, Name = "John", Age = 20, Email = "a@a.com", PhoneNumber = "1" }));
        }

        [TestMethod]
        public void GetStudentById_InvalidId_ThrowsException()
        {
            AssertThrows<CustomException>(() => _manager.GetStudentById(0));
        }

        [TestMethod]
        public void DeleteStudent_InvalidId_ThrowsException()
        {
            AssertThrows<CustomException>(() => _manager.DeleteStudent(0));
        }
    }
}
