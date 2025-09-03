using ClientManager.src.Infrastructure;
using ClientManager.src.Interfaces;
using ClientManager.src.Repositories;
using ClientManager.src.Services;
using Microsoft.EntityFrameworkCore;
using ClientManager.src.Middlewares;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace ClientManager; 
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // CONFIGURAÇÃO DO DATABASE CONTEXT
        builder.Services.AddDbContext<AplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // CONFIGURAÇÃO DE CORS
        builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        }));

        builder.Services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // INJEÇÃO DE DEPENDÊNCIA
        builder.Services.AddScoped<ClienteService>();
        builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

        builder.Services.AddHttpContextAccessor();

        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.UseCors();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseAuthorization(); 
        app.MapControllers();

        app.Run();
    }
}
