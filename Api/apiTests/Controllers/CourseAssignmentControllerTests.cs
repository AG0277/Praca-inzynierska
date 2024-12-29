using api;
using Domain.Models;
using Infastructure;
using Logic.Dto.CourseAssigment;
using Logic.Dto.RoomReservation;
using Logic.Dto.RoomReservationDto;
using Logic.Dto.WorkerAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TestcontainersForDotNetAndDocker.Tests;

namespace apiTests.Controllers
{

    public class CourseAssignmentControllerTests : IClassFixture<ApplicationTestsFactory>
    {
        private readonly HttpClient _client;
        private readonly HostelDbContext db;
        private readonly JsonSerializerOptions jsonOptions;
        private readonly ApplicationTestsFactory _factory;

        public CourseAssignmentControllerTests(ApplicationTestsFactory Factory)
        {
            _client = Factory.CreateClient();
            jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            db = Factory._db;
            _factory = Factory;
        }

        [Fact]
        public async void CourseAssignment_CreateCourseAssignment_BadRequest()
        {
            //Arrange
            var url = "/api/CourseAssigment/CreateCourseAssigment";

            var courseAssignmentDto = new CreateCourseAssigmentDto()
            {
                CourseId = 1,
                InstructorId = 1,
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(3)
            };

            var json = JsonSerializer.Serialize(courseAssignmentDto);
            var jsonContent = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );


            // Act

            var response = await _client.PostAsync(url, jsonContent);

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async void CourseAssignment_CreateCourseAssignment_Ok()
        {
            //Arrange
            var url = "/api/CourseAssigment/CreateCourseAssigment";
            var token = await _factory.GetAdminUserToken();

            var courseAssignmentDto = new CreateCourseAssigmentDto()
            {
                CourseId = 1,
                InstructorId = 1,
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(3)
            };

            var json = JsonSerializer.Serialize(courseAssignmentDto);
            var jsonContent = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = jsonContent
            };
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


            // Act

            var response = await _client.SendAsync(request);

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public async void CourseAssignment_DeleteCourseAssignment_Ok()
        {
            //Arrange
            var url = "/api/CourseAssigment/DeleteCourseAssigment/3";
            var token = await _factory.GetAdminUserToken();
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


            // Act

            var response = await _client.SendAsync(request);

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }


        [Fact]
        public async void CourseAssignment_DeleteCourseAssignment_BadRequest()
        {
            //Arrange
            var url = "/api/CourseAssigment/DeleteCourseAssigment/33";
            var token = await _factory.GetAdminUserToken();
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


            // Act

            var response = await _client.SendAsync(request);

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
        }
    }
}
