using BHDAPI.Data.Interfaces;
//using BHDAPI.Data.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin",
            builder => builder.WithOrigins("http://localhost:3000")
                            .AllowCredentials()
                            .AllowAnyHeader()
                            .AllowAnyMethod());
    });
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers()    
.AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    }); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BoardingHouseContext>(db => db.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Singleton);
builder.Services.AddTransient<BoardingHouseContext>();

builder.Services.AddScoped<IBoardingHouseService, BoardingHouseService>();
builder.Services.AddScoped<IRentService, RentService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IRepairService, RepairService>();
builder.Services.AddScoped<ICostService, CostService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Apply the CORS policy globally       
app.UseCors("AllowSpecificOrigin");

app.MapControllers(); 

app.Run();
