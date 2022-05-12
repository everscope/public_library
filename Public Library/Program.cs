using Microsoft.EntityFrameworkCore;
using Public_Library;
using Public_Library.Controllers;
using Public_Library.DAL;
using Public_Library.LIB;
using Public_Library.LIB.Interfaces;
using Serilog;
using Serilog.Filters;
using Serilog.Formatting.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PublicLibraryContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));
builder.Services.AddTransient<IDatabaseReader, DatabaseReader>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSingleton<AttendanceAmount>();
//builder.Services.AddControllers().AddJsonOptions(x =>
//   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Destructure.ByTransforming<PatronInputModel>(
        p => new {Name = p.Name, Surname = p.Surname, Email = p.Email, Password = p.Password})
    .Destructure.ByTransforming<BookInputModel>(
        p => new {Title = p.Title, Author = p.Author})
    .WriteTo.Console()
    .WriteTo.File("Logs/AllLogs/AllLogs.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.File(new JsonFormatter(), "JsonLogs/JsonAllLogs/AllLogs.json",
                                                rollingInterval: RollingInterval.Day)
    .WriteTo.Logger(patronLogger => patronLogger
                    .Filter.ByIncludingOnly(Matching.FromSource<PatronController>()))
                    .WriteTo.File("Logs/PatronLogs/PatronLog.txt", rollingInterval: RollingInterval.Day)
                    .WriteTo.File(new JsonFormatter(), "JsonLogs/JsonPatronLogs/PatronLogs.json", 
                                                rollingInterval: RollingInterval.Day)
    .WriteTo.Logger(bookLogger => bookLogger
                    .Filter.ByIncludingOnly(Matching.FromSource<BookController>()))
                    .WriteTo.File("Logs/BookLogs/BookLog.txt", rollingInterval: RollingInterval.Day)
                    .WriteTo.File(new JsonFormatter(), "JsonLogs/JsonBookLogs/BookLogs.json",
                                                rollingInterval: RollingInterval.Day)
    //.WriteTo.Logger(attandanceLogger => attandanceLogger
    //                .Filter.ByIncludingOnly(Matching.FromSource<AttendanceController>()))
    //                .WriteTo.File("Logs/AttendanceLogs/AttendanceLogs.txt", rollingInterval: RollingInterval.Day)
    //                .WriteTo.File(new JsonFormatter(), "JsonLogs/JsonAttendanceLogs/AttendanceLogs.json",
    //                                            rollingInterval: RollingInterval.Day)
    .CreateLogger();

var app = builder.Build();


using (var db = new PublicLibraryContext(new DbContextOptionsBuilder()
    .UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")).Options))
{
    db.Database.EnsureCreated();
}

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
