
using ImagePortal.DataAccess.ImageData;
using ImagePortal.DataContext.Context;
using ImagePortal.Services.ApiServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager cfg = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ImageDataContext>(options =>
{
    options.UseSqlServer(cfg.GetConnectionString("Default"), o => o.CommandTimeout(180));
}, optionsLifetime: ServiceLifetime.Transient, contextLifetime: ServiceLifetime.Transient);

builder.Services.AddTransient<IImageDataRepository, ImageDataRepository>();
builder.Services.AddTransient<IImageDataService, ImageDataService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
.AllowAnyHeader()
.AllowAnyMethod()
.SetIsOriginAllowed((host) => true)
.AllowCredentials());


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
