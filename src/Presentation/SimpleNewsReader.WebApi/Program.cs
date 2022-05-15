using FluentValidation;
using SimpleNewsReader.Application;
using SimpleNewsReader.Domain.Common;
using SimpleNewsReader.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var newsSettings = builder.Configuration.GetSection("ConnectionString").Get<NewsSettings>();
string connectionString;
#if DEBUG
connectionString = newsSettings.LocalSqlServer ?? "";
#else
connectionString = newsSettings.DevelopSqlServer;
#endif

// Add services to the container.
builder.Services.AddDbContext(connectionString);
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
