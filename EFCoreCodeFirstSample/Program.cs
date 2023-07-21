using EFCoreCodeFirstSample.Helpers;
using EFCoreCodeFirstSample.Models;
using EFCoreCodeFirstSample.Repository.Interface;
using EFCoreCodeFirstSample.Repository.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
{
    var isMigration = builder.Configuration.GetValue("Migration:Enable", false);
    var isSeedingDataEnabled = builder.Configuration.GetValue("SeedData:Enable", false);
    var isTestDataEnabled = builder.Configuration.GetValue("TestData:Enable", false);
    var seedDataPath = builder.Configuration.GetValue<string>("SeedData:Path");
    var isProduction = builder.Configuration.GetValue("IsProduction:Enable", false);
    //var host = CreateHostBuilder(args).Build();

    var services = builder.Services;

    services.AddCors();
    services.AddControllers().AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        // ignore omitted parameters on models to enable optional params (e.g. User update)
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

    // Connection String
    var connectionString = builder.Configuration.GetConnectionString("EFCoreCodeFirstSampleeDB");

    services.AddDbContext<EFCoreCodeContext>(options => options.UseSqlServer(connectionString));

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    // Add services to the container configure DI for application services
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.AddScoped<IEmployeeService, EmployeeService>();
    services.AddScoped<IMemberContactDetailService, MemberContactDetailService>();
}

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    app.MapControllers();
}

app.Run();
