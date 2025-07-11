using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoWrapper;
using Microsoft.EntityFrameworkCore;
using University.API.Modules;
using University.Data.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new ServiceModule());
        builder.RegisterModule(new RepositoryModule());
    });
// Add services to the container.

builder.Services.AddDbContext<UniversityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseApiResponseAndExceptionWrapper();
app.MapControllers();

app.Run();