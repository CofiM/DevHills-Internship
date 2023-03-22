using Microsoft.EntityFrameworkCore;
using WokerShop.Services.Services;
using WorkerShop.Core.Interfaces;
using WorkerShop.Repository.DbContexts;
using WorkerShop.Repository.Implementation;
using WorkerShop.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WorkerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AutoServiceCS")));

builder.Services.AddTransient<IWorkerService, WorkerServices>();
builder.Services.AddTransient<IWorkerRepository,WorkerRepository>();

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
