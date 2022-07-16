using System.Text;
using api.Data;
using api.Interfaces;
using api.Services;
using api.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

// Add services to the container.
services.AddScoped<ITokenService, TokenService>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<ICityRepository, CityRepository>();
services.AddScoped<IFlightRepository, FlightRepository>();
services.AddScoped<IReservationRepository, ReservationRepository>();

services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(config.GetConnectionString("DefaultConnection"));
});

services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
);

services.AddCors();

services.AddSignalR().AddNewtonsoftJsonProtocol(options => 
    options.PayloadSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins("https://localhost:4200"));

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<MainHub>("mainHub");

app.Run();
