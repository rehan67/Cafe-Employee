using Cafe_Employee.Business_Layer.CafeBL;
using Cafe_Employee.Business_Layer.EmployCafe;
using Cafe_Employee.Business_Layer.EmployeeBL;
using Cafe_Employee.Data;
using Cafe_Employee.Data_Layer.CafeDL;
using Cafe_Employee.Data_Layer.EmployCafe;
using Cafe_Employee.Data_Layer.EmployeeDL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



// Register repositories and services

// Cafe
builder.Services.AddScoped<ICafeRepository, CafeRepository>();
builder.Services.AddScoped<ICafeService, CafeService>();

// Employee
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeCafeRepository, EmployeeCafeRepository>();
builder.Services.AddScoped<IEmployeeCafeService, EmployeeCafeService>();


builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Enable CORS middleware
app.UseCors("AllowAllOrigins");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
