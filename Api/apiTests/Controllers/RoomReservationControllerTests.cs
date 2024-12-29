using api;
using Domain.Models;
using Infastructure;
using Infastructure.Repositories;
using Logic.Dto.RoomReservation;
using Logic.Dto.RoomReservationDto;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text;
using System.Text.Json;
using TestcontainersForDotNetAndDocker.Tests;

namespace apiTests.Controllers
{
    public class RoomReservationControllerTests: IClassFixture<ApplicationTestsFactory>
    {
        private readonly HttpClient _client;
        private readonly HostelDbContext db;
        private readonly JsonSerializerOptions jsonOptions;

        public RoomReservationControllerTests(ApplicationTestsFactory Factory)
        {
            _client = Factory.CreateClient();
            jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            db = Factory._db;
        }

        [Fact]
        public async void RoomReservation_CreateReservation_BadRequest()
        {
            //Arrange
            var url = "/api/RoomReservation/CreateRoomReservation";

            var roomReservationDto = new CreateRoomReservationDto
            {
                AppUserId = 1,
                RoomId = 4,
                NumberOfBeds = 3,
                TotalPrice = 120,
                From = DateTime.Now.AddDays(1), 
                To = DateTime.Now.AddDays(5)  
            };

            var json = JsonSerializer.Serialize(roomReservationDto);
            var jsonContent = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );


            // Act

            var response = await _client.PostAsync(url,jsonContent);

            var errorContent = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ApiErrorResponse>(errorContent);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void RoomReservation_CreateReservation_Ok()
        {
            //Arrange
            var url = "/api/RoomReservation/CreateRoomReservation";
            var userUrl = "/api/AppUser/GetUserByName?FirstName=Mariusz&LastName=Pudzianowski";

            var roomReservationDto = new CreateRoomReservationDto
            {
                AppUserId = 1,
                RoomId = 4,
                NumberOfBeds = 1,
                TotalPrice = 150*4,
                From = DateTime.UtcNow.AddDays(3),
                To = DateTime.UtcNow.AddDays(7)
            };

            var json = JsonSerializer.Serialize(roomReservationDto);
            var jsonContent = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            var user = await db.AppUsers.FindAsync(1);
            var userAmountPaid = user.AmountPaid;
            var userAmountTobePaird = user.AmountToBePaid;


            // Act

            var response = await _client.PostAsync(url, jsonContent);

            var content = await response.Content.ReadAsStringAsync();
            var responseRoom = JsonSerializer.Deserialize<GetRoomReservationDto>(content, jsonOptions);
            response.EnsureSuccessStatusCode();


            // Assert
            var userResponse = await _client.GetAsync(userUrl);
            var userContent = await userResponse.Content.ReadAsStringAsync();
            var userAfter = JsonSerializer.Deserialize<AppUser>(userContent, jsonOptions);
            var room = await db.Rooms.FindAsync(4);

            Assert.NotNull(userAfter);
            Assert.NotNull(room);
            Assert.True(user.AmountToBePaid + room.PricePerBed * 4  == userAfter.AmountToBePaid);
            Assert.True(room.PricePerBed * 4  == responseRoom.TotalPrice);
            Assert.NotNull(responseRoom);
        }
    }
}
