using api;
using Domain.Models;
using Infastructure;
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
    public class AccountControllerTests : IClassFixture<ApplicationTestsFactory>
    {
        private readonly HttpClient _client;
        private readonly HostelDbContext db;
        private readonly JsonSerializerOptions jsonOptions;
        private readonly ApplicationTestsFactory _factory;

        public AccountControllerTests(ApplicationTestsFactory Factory)
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
        public async void AccountController_Login_Unauthorized()
        {
            //Arrange
            var url = "/api/account/login";

            var loginDto = new LoginDto()
            {
                Username = "Parówka123",
                Password = "asdQWE123!@#"
            };


            var json = JsonSerializer.Serialize(loginDto);
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
        public async void AccountController_Login_Ok()
        {
            //Arrange
            var url = "/api/account/login";

            var loginDto = new LoginDto()
            {
                Username = "adminuser",
                Password = "asdQWE123!@#"
            };


            var json = JsonSerializer.Serialize(loginDto);
            var jsonContent = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );


            // Act

            var response = await _client.PostAsync(url, jsonContent);


            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public async void AccountController_Register_Ok()
        {
            
            //Arrange
            var urlRegister = "/api/account/Register";
            var token = await _factory.GetAdminUserToken();


            var registerDto = new RegisterDto()
            {
                Username = "test",
                Password = "asdQWE123!@#"
            };


            var json = JsonSerializer.Serialize(registerDto);
            var jsonContent = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            var request = new HttpRequestMessage(HttpMethod.Post, urlRegister)
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
        public async void AccountController_Register_Unauthorized()
        {
            //Arrange
            var url = "/api/account/register";

            var loginDto = new LoginDto()
            {
                Username = "test",
                Password = "asdQWE123!@#"
            };


            var json = JsonSerializer.Serialize(loginDto);
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
    }
}
