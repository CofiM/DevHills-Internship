using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WokerShop.Services.Services;
using WorkerShop.Core.Interfaces;
using WorkerShop.Repository.DbContexts;
using WorkerShop.Repository.Implementation;
using WorkerShop.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Workers",
        Description = "Description"
    });
    //i sa onaj executing isti eror
    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
    var commentsFileName = "Worker.API" + ".xml";
    var commentsFile = Path.Combine(baseDirectory, commentsFileName);
    //options.IncludeXmlComments(commentsFile);

});


builder.Services.AddDbContext<WorkerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AutoServiceCS")));

builder.Services.AddTransient<IWorkerService, WorkerServices>();
builder.Services.AddTransient<IWorkerRepository,WorkerRepository>();
builder.Services.AddTransient<IClientRepository, ClientRepository>();
builder.Services.AddTransient<IClientService, ClientService>();

//ask sasko (GetAssemblies())
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
