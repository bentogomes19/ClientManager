using ClientManager.src.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CONFIGURA��O DO DATABASE CONTEXT
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// CONFIGURA��O DE CORS
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
{
    policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyMethod();
}));



var app = builder.Build();

// Configure the HTTP request pipeline.
// CONFIGURA��O DO SWAGGER
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
