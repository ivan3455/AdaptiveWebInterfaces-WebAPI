using AdaptiveWebInterfaces_WebAPI.Data.Database;
using AdaptiveWebInterfaces_WebAPI.Hubs;
using AdaptiveWebInterfaces_WebAPI.Services;
using AdaptiveWebInterfaces_WebAPI.Services.Background;
using AdaptiveWebInterfaces_WebAPI.Services.Currency;
using AdaptiveWebInterfaces_WebAPI.Services.Health_Check;
using AdaptiveWebInterfaces_WebAPI.Services.HealthCheck;
using AdaptiveWebInterfaces_WebAPI.Services.Jwt;
using AdaptiveWebInterfaces_WebAPI.Services.PasswordHasher;
using AdaptiveWebInterfaces_WebAPI.Services.Quartz;
using AdaptiveWebInterfaces_WebAPI.Services.Weather;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Quartz;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;
using OpenTelemetry;
using OpenTelemetry.Instrumentation.AspNetCore;
using OpenTelemetry.Instrumentation.Http;
using OpenTelemetry.Instrumentation.SqlClient;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}

var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
{
    throw new InvalidOperationException("JWT key not found in configuration.");
}

builder.Services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddSingleton<IJwtService, JwtService>();

builder.Services.AddSingleton<ICurrencyService, CurrencyService>();
builder.Services.AddSignalR();

builder.Services.AddSingleton<ServiceHealthCheck>();
builder.Services.AddSingleton<ResourceUsageHealthCheck>();
builder.Services.AddSingleton<ICustomHealthCheckResultWriter, CustomHealthCheckResultWriter>();

builder.Services.AddDbContext<EmailListenerDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddHealthChecks()
    .AddCheck<ServiceHealthCheck>("service_check", tags: new[] { "service" })
    .AddCheck<ResourceUsageHealthCheck>("resource_usage_check", tags: new[] { "resource" })
    .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), name: "sql_check", tags: new[] { "sql" });

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecksUI().AddInMemoryStorage();
builder.Services.AddHttpClient();

builder.Services.AddQuartzServices();
builder.Services.Configure<EmailJob>(builder.Configuration.GetSection("SendGrid"));

builder.Services.AddScoped<ITestTableService, TestTableService>();

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddSingleton<IWeatherService, WeatherService>();

// WeatherBackgroundService як фонова служба для періодичного отримання даних про погоду та кешування результатів
builder.Services.AddHostedService<WeatherBackgroundService>();

// Реєстрація NotificationBackgroundService як фонової служби для періодичного надсилання повідомлень клієнтам через SignalR
builder.Services.AddHostedService<NotificationBackgroundService>();

builder.Services.AddMemoryCache();

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService("MyAspNetCoreService"))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddSqlClientInstrumentation()
        .AddSource("AdaptiveWebInterfaces_WebAPI")
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("MyAspNetCoreService"))
        .SetSampler(new TraceIdRatioBasedSampler(0.1)) // 10% слідів
        .AddProcessor(new SimpleActivityExportProcessor(new CustomTraceExporter())) // Додаємо власний експортер
        .AddOtlpExporter(options =>
        {
            options.Endpoint = new Uri("http://localhost:4317");
        }))
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddOtlpExporter(options =>
        {
            options.Endpoint = new Uri("http://localhost:4317");
        }));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.OAuthClientId("swagger");
        c.OAuthAppName("Your API - Swagger");

        c.EnableDeepLinking();
        c.DisplayOperationId();
        c.DisplayRequestDuration();
        c.DocExpansion(DocExpansion.None);
        c.EnableFilter();
        c.ShowExtensions();
        c.EnableValidator();
        c.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put, SubmitMethod.Delete, SubmitMethod.Patch);
    });
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

try
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<EmailListenerDbContext>();
        dbContext.Database.EnsureCreated();
        Console.WriteLine("Connected to the database successfully!");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to connect to the database: {ex.Message}");
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDefaultFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// NotificationHub SignalR
app.MapHub<NotificationHub>("/notificationHub");
app.MapHub<CurrencyHub>("/currencyHub");

var customHealthCheckResultWriter = app.Services.GetRequiredService<ICustomHealthCheckResultWriter>();

app.UseHealthChecks("/service-health", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("service"),
    ResponseWriter = customHealthCheckResultWriter.WriteResponse
});

app.UseHealthChecks("/resource-health", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("resource"),
    ResponseWriter = customHealthCheckResultWriter.WriteResponse
});

app.UseHealthChecks("/sql-health", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("sql"),
    ResponseWriter = customHealthCheckResultWriter.WriteResponse
});

app.UseHealthChecksUI();

app.Run();
