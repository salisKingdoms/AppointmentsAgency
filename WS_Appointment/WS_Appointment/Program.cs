using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WS_Appointment.Config;
using WS_Appointment.EFCore;
using WS_Appointment.Feature.dao;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "My Assesment",
        Description = " an ASP.NET Web API",
        Contact = new OpenApiContact
        {
            Name = "Salis",
            Email = "aryanisalis095@gmail.com"
        },
        License = new OpenApiLicense
        {
            Name = "License by salis"
        }
    });

    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorize Bearer",
        In = ParameterLocation.Header,
        Name = "token",
        Type = SecuritySchemeType.ApiKey
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id="oauth2"
            }
           },new string[]{}
        }

    });

});

var config = new AppConfig();
builder.Configuration.Bind("AppConfig", config);
builder.Services.AddSingleton(config);

//add jwttoken soon
//builder.Services.AddSingleton<IJwtFunction, JwtFunction>();
// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;

    services.AddCors();
    services.AddControllers().AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        // ignore omitted parameters on models to enable optional params (e.g. User update)
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

    // connect dbcontext postgresql
    services.AddDbContext<EF_DataContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("DbSettings")));
    // configure DI for application services
    services.AddScoped<IActivityRepo, ActivityRepo>();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();