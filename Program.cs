using Microsoft.OpenApi.Models;
using AdaptiveWebInterfaces_WebAPI.Services.User;
using AdaptiveWebInterfaces_WebAPI.Services.Good;
using AdaptiveWebInterfaces_WebAPI.Services.Manufacturer;
using AdaptiveWebInterfaces_WebAPI.Services.Category;
using AdaptiveWebInterfaces_WebAPI.Services.Car;
using AdaptiveWebInterfaces_WebAPI.Services.Order;
using AdaptiveWebInterfaces_WebAPI.Services.OrderDetail;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" })
);

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IGoodService, GoodService>();
builder.Services.AddSingleton<IManufacturerService, ManufacturerService>();
builder.Services.AddSingleton<ICategoryService, CategoryService>();
builder.Services.AddSingleton<ICarService, CarService>();
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<IOrderDetailService, OrderDetailService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
