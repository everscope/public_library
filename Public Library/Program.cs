using Microsoft.EntityFrameworkCore;
using Public_Library;
using Public_Library.DAL;
using Public_Library.LIB;
using Public_Library.LIB.Interfaces;
using Public_Library.Maps;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PublicLibraryContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));
builder.Services.AddTransient<IDatabaseReader, DatabaseReader>();
builder.Services.AddAutoMapper(typeof(PatronProfile));

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
