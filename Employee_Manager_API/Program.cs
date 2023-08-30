using Employee_Manager_API.DbClass;
using Employee_Manager_API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EmpDbClass>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});

builder.Services.AddCors(options => 
{
    options.AddPolicy(name: "AllowOrigin", builder => 
    {
        builder.WithOrigins().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowOrigin");

app.MapControllers();

app.Run();
