using FluentValidation;
using SimpleNewsReader.Application;
using SimpleNewsReader.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddDbContext(builder.Configuration.GetConnectionString("DefaultConnectionString"));
builder.Services.ResolveServices();
var applicationAssembly = typeof(AssemblyReferences).Assembly;
builder.Services.AddAutoMapper(applicationAssembly);
builder.Services.AddValidatorsFromAssembly(applicationAssembly);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint("/swagger/v1/swagger.json","Simple News Reader API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
