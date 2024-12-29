using api;
using apiTests;
using Docker.DotNet.Models;
using Domain.Models;
using DotNet.Testcontainers.Builders;
using Infastructure;
using Logic.Dto.RoomReservation;
using Logic.Dto.WorkerAccount;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text;
using System.Text.Json;
using Testcontainers.PostgreSql;

namespace TestcontainersForDotNetAndDocker.Tests;

public class ApplicationTestsFactory : WebApplicationFactory<Program>, IAsyncLifetime
{

    protected readonly PostgreSqlContainer _postgres;
    public HostelDbContext _db;
    public ApplicationTestsFactory()
    {
        _postgres = new PostgreSqlBuilder()
            .WithImage("postgres:16.3")
            .WithDatabase("db")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .WithEnvironment("PGOPTIONS", "--client_min_messages=debug")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilCommandIsCompleted("pg_isready"))
            .WithCleanUp(true)
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _postgres.StartAsync().GetAwaiter().GetResult();
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<HostelDbContext>));

            services.AddDbContext<HostelDbContext>(options =>
                options.UseNpgsql(_postgres.GetConnectionString()));
        });
    }

    public async Task InitializeAsync()
    {
        await _postgres.StartAsync();
        _db = Services.CreateScope().ServiceProvider.GetRequiredService<HostelDbContext>();
        _db.Database.EnsureCreated();

        var config = Services.CreateScope().ServiceProvider.GetRequiredService<IConfiguration>();
        var connectionStringsSection = config.GetSection("ConnectionStrings");
        if (connectionStringsSection["ConnectionStrings"] != null)
        {
            var section = _postgres.GetConnectionString()+ ";Include Error Detail=true;";
            connectionStringsSection["ConnectionStrings"] = section;
        }
        DatabaseSeeder.Seed(_db);

        using (var serviceScope = Services.CreateScope())
        {
            var services = serviceScope.ServiceProvider;
            var userManager = services.GetRequiredService<UserManager<WorkerAccount>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            ApplicationDbInitializer.Initialize(services, userManager, roleManager).Wait();
        }
    }

    public new async Task DisposeAsync()
    {
        await _postgres.DisposeAsync();

    }

    public async Task<string> GetAdminUserToken()
    {
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var client = CreateClient();

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

        var response = await client.PostAsync(url, jsonContent);
        var content = await response.Content.ReadAsStringAsync();
        var responseRoom = JsonSerializer.Deserialize<UserWithToken>(content, jsonOptions);
        return responseRoom?.Token??"";
    }

    public HostelDbContext GetDb()
    {
        var scope = Services.CreateScope();
        return scope.ServiceProvider.GetRequiredService<HostelDbContext>();
    }
}