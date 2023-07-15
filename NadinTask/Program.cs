using Microsoft.EntityFrameworkCore;
using NadinTask.API.Extensions;
using NadinTask.API.Services;
using NadinTask.Domain.Models.Security;
using NadinTask.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => 
{
    options.AddPolicy("CorsPolicy", builder => builder.WithOrigins("https://localhost:3000/")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .SetIsOriginAllowed((host) => true));
});
//extensionFolderServices
builder.Services.ConfigureIdentity();
builder.Services.RegisterRepositoryDependencies(); 
builder.Services.RegisterServicesDependencies(); 
builder.Services.ConfigureMappingProfiles();

builder.Services.ConfigureSqlContext(builder.Configuration);
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<NadinContext>();
    dataContext.Database.Migrate();
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
