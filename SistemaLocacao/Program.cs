using Microsoft.EntityFrameworkCore;
using SistemaLocacao.Context;
using SistemaLocacao.Repositories.Client;
using SistemaLocacao.Repositories.Location;
using SistemaLocacao.Repositories.Movie;
using SistemaLocacao.Repositories.Report;
using SistemaLocacao.Services.Client;
using SistemaLocacao.Services.Location;
using SistemaLocacao.Services.Movie;
using SistemaLocacao.Services.Report;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//ConnectionString
var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];

builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 28))));

//Service
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IReportService, ReportService>();

//Repository
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();

builder.Services.AddControllers();

builder.Services.AddCors(c =>
{
    c.AddPolicy("CorsPolicy", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
