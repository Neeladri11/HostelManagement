using HostelManagement.BAL.Contracts;
using HostelManagement.BAL.Services;
using HostelManagement.DAL.Data;
using HostelManagement.DAL.DataAccess;
using HostelManagement.DAL.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//adding db context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("HostelDB"),b => b.MigrationsAssembly("HostelManagement.API"));
});

builder.Services.AddScoped<IHostelManager, HostelManager>();
builder.Services.AddScoped<IRoomManager, RoomManager>();
builder.Services.AddScoped<IStudentManager, StudentManager>();
builder.Services.AddScoped<IMealManager, MealManager>();
builder.Services.AddScoped<IBookingManager, BookingManager>();
builder.Services.AddScoped<IPaymentManager, PaymentManager>();
//Inject Data Access to our Program
builder.Services.AddScoped<IDataAccess, DataAccess>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

var loggerfactory = app.Services.GetService<ILoggerFactory>();
loggerfactory.AddFile(builder.Configuration["Logging:LogFilePath"].ToString());

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
