using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WebAPI.Proper.Request.Response.Controllers;
using WebAPI.Proper.Request.Response.Models;
using WebAPI.Proper.Request.Response.Models.Students;
using WebAPI.Proper.Request.Response.Repository.Interface;
using Xunit;

namespace WebAPI.Proper.Request.Response.xUnit.Tests
{
    public class StudentsControllerTests
    {
        private readonly StudentsController _studentsController;
        private readonly Mock<IStudentServices> _studentService = new Mock<IStudentServices>();

        public StudentsControllerTests()
        {
            _studentsController = new StudentsController(_studentService.Object);
        }

        [Fact]
        public async Task GetStudents_Return_Success()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory().ToString(), "MockData//");


            // var responseItem = MockReader<GenericResponse<IEnumerable<Students>>>(path + "student.json");

            var studentData = new List<Students>()
            {
                new Students()
                {
                    Address = "Gurgaon, Haryana, India",
                    Age = 22,
                    CreatedDate = DateTime.Now.Date,
                    Email = "ashok@gmail.com",
                    FirstName = "Ashok",
                    LastName = "Kumar",
                    Gender = Models.Enums.Gender.Male,
                    ID = 1,
                    IsActive = true,
                    IsDeleted = false,
                    Mobile = "8989278989",
                    UpdatedDate = DateTime.Now.Date,
                    ZipCode = "122006"
                },
                new Students()
                {
                    Address = "Harnathpur Chauk, Pakridayal, East Champaran",
                    Age = 42,
                    CreatedDate = DateTime.Now.Date,
                    Email = "d.nath@yahoo.co",
                    FirstName = "Dinesh",
                    LastName = "Kumar",
                    Gender = Models.Enums.Gender.Male,
                    ID = 1,
                    IsActive = true,
                    IsDeleted = false,
                    Mobile = "1234567890",
                    UpdatedDate = DateTime.Now.Date,
                    ZipCode = "123457"
                }
            };

            var responseItem = new GenericResponse<IEnumerable<Students>>()
            {
                Count = 1,
                Errors = null,
                IsValid = true,
                Message = "",
                StatusCode = 200,
                Result = studentData
            };

            _studentService.Setup(service => service.Listing()).ReturnsAsync(responseItem);

            var response = await _studentsController.GetStudents();
            response.StatusCode.Equals(HttpStatusCode.OK);
            _studentService.Verify(service => service.Listing(), Times.Exactly(0));
        }

        private static Task<T> MockReader<T>(string filePath)
        {
            try
            {
                using FileStream stream = File.OpenRead(filePath);
                using (StreamReader reader = new StreamReader(stream))
                using (JsonTextReader jsonReader = new JsonTextReader(reader))
                {
                    JsonSerializer ser = new JsonSerializer();
                    return Task.FromResult(ser.Deserialize<T>(jsonReader));
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
