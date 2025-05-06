using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Repositories;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Here Dependency injection is done by injecting 'NZWalksDbContext' which implements 'DbContext' interface.
// which implies 'DbContext' injection is done via instance of 'NZWalksDbContext' class.
builder.Services.AddDbContext<NZWalksDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NZConnectionsString")));

// Injecting IRegionRepository of type SQLRegionRepository
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
// Injecting IWalkRepository of type SQLWalkRepository
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();
//builder.Services.AddScoped<IRegionRepository, InMemoryRegionRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

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
