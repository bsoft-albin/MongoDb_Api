using MongoDb_Api.DbEngine;
using MongoDb_Api.Frameworks.CommonMeths;
using MongoDb_Api.Repositories;
using MongoDb_Api.Repositories.QuickChatRepo;
using MongoDb_Api.Services;
using MongoDb_Api.Services.QuickChatService;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IMongoSampleRepo, MongoSampleRepo>();

builder.Services.AddSingleton<IMongoSampleService, MongoSampleService>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration["MongoDbSettings:ConnectionString"] ?? "";
    var databaseName = configuration["MongoDbSettings:DatabaseName"] ?? "";
    return new MongoSampleService(connectionString, databaseName);
});

builder.Services.AddSingleton<IMongoCloudEngine, MongoCloudEngine>(sp => {

    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration["MongoDbSettings:ConnectionString"] ?? "";
    var databaseName = configuration["MongoDbSettings:DatabaseName"] ?? "";
    return new MongoCloudEngine(connectionString, databaseName);
});

builder.Services.AddScoped<IRegistrationRepo, RegistrationRepo>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAllOrigins",
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                      });
});

WebApplication app = builder.Build();

// Initialize LogHelper with hosting environment
ErrorLogger.Initialize(app.Services.GetRequiredService<IHostEnvironment>());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();
