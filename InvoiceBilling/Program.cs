using DBLayer.Models;
using DBLayer.Repository.Interface;
using DBLayer.Repository.Service;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using UtilitiesLayer.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
            builder => builder.WithOrigins("*")
                              .AllowAnyHeader().WithExposedHeaders("Content-Disposition").WithExposedHeaders("jwttoken")
                              .AllowAnyMethod());
});

var AMSConnectionString = ConfigurationUtility.GetSetting("ConnectionString");
builder.Services.AddDbContext<InvoiceBillingContext>(options =>
    options.UseSqlServer(AMSConnectionString));



builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IProductService, ProductService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
