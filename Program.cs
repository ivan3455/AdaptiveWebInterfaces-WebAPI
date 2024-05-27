using AdaptiveWebInterfaces_WebAPI.Data.Database;
using AdaptiveWebInterfaces_WebAPI.Hubs;
using AdaptiveWebInterfaces_WebAPI.Services.Auth;
using AdaptiveWebInterfaces_WebAPI.Services.Car;
using AdaptiveWebInterfaces_WebAPI.Services.Category;
using AdaptiveWebInterfaces_WebAPI.Services.Currency;
using AdaptiveWebInterfaces_WebAPI.Services.Excel;
using AdaptiveWebInterfaces_WebAPI.Services.Good;
using AdaptiveWebInterfaces_WebAPI.Services.Health_Check;
using AdaptiveWebInterfaces_WebAPI.Services.HealthCheck;
using AdaptiveWebInterfaces_WebAPI.Services.Jwt;
using AdaptiveWebInterfaces_WebAPI.Services.Manufacturer;
using AdaptiveWebInterfaces_WebAPI.Services.Order;
using AdaptiveWebInterfaces_WebAPI.Services.OrderDetail;
using AdaptiveWebInterfaces_WebAPI.Services.PasswordHasher;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;

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

builder.Services.AddSingleton<IGoodService, GoodService>();
builder.Services.AddSingleton<IManufacturerService, ManufacturerService>();
builder.Services.AddSingleton<ICategoryService, CategoryService>();
builder.Services.AddSingleton<ICarService, CarService>();
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddTransient<IExcelService, ExcelService>();
builder.Services.AddSingleton<IJwtService, JwtService>();

builder.Services.AddSingleton<ICurrencyService, CurrencyService>();
builder.Services.AddSignalR();

builder.Services.AddSingleton<ServiceHealthCheck>();
builder.Services.AddSingleton<ResourceUsageHealthCheck>();
builder.Services.AddSingleton<ICustomHealthCheckResultWriter, CustomHealthCheckResultWriter>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(connectionString));

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
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "WebAPI", Version = "v1" });

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
    .AddMySql(builder.Configuration.GetConnectionString("DefaultConnection"), name: "mysql_check", tags: new[] { "mysql" });

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecksUI().AddInMemoryStorage();

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
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
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

app.MapHub<CurrencyHub>("/currencyHub");

var customHealthCheckResultWriter = app.Services.GetRequiredService<ICustomHealthCheckResultWriter>();

app.UseHealthChecks("/service-health", new HealthCheckOptions
{
    Predicate = (check) => check.Tags.Contains("service"),
    ResponseWriter = customHealthCheckResultWriter.WriteResponse
});

app.UseHealthChecks("/res-health", new HealthCheckOptions
{
    Predicate = (check) => check.Tags.Contains("resource"),
    ResponseWriter = customHealthCheckResultWriter.WriteResponse
});

app.UseHealthChecks("/db-health", new HealthCheckOptions
{
    Predicate = (check) => check.Tags.Contains("mysql"),
    ResponseWriter = customHealthCheckResultWriter.WriteResponse
});

app.UseHealthChecksUI(config => config.UIPath = "/hc-ui");

app.Run();
