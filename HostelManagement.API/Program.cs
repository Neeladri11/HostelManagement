using HostelManagement.BAL.Contracts;
using HostelManagement.BAL.Services;
using HostelManagement.DAL.Data;
using HostelManagement.DAL.DataAccess;
using HostelManagement.DAL.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//adding db context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("HostelDB"),b => b.MigrationsAssembly("HostelManagement.API"));
});

builder.Services.AddScoped<IHostelManager, HostelManager>();
//Inject Data Access to our Program
builder.Services.AddScoped<IDataAccess, DataAccess>();

// Add services to the container.

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

app.MapControllers();

app.Run();
