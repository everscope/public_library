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
builder.Services.AddDbContextFactory<PublicLibraryContext>();
//builder.Services.AddDbContext<PublicLibraryContext, PublicLibraryContext>();
builder.Services.AddTransient<IDatabaseReader, DatabaseReader>();
builder.Services.AddAutoMapper(typeof(PatronProfile));

using (var db = new PublicLibraryContextFactory().CreateDbContext(new string[0]))
{
    db.Database.EnsureCreated();
}

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
