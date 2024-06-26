using BHDAPI.Data.Interfaces;
//using BHDAPI.Data.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BoardingHouseContext>(db => db.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Singleton);
builder.Services.AddTransient<BoardingHouseContext>();

builder.Services.AddScoped<IBoardingHouseService, BoardingHouseService>();
builder.Services.AddScoped<IRentService, RentService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IRepairService, RepairService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers(); 

app.Run();
