using Microsoft.EntityFrameworkCore;
using Property_Search_Web_App.Common_Methods;
using Property_Search_Web_App.Data;
using Property_Search_Web_App.Repository.Implementation;
using Property_Search_Web_App.Repository.Interface;
using Property_Search_Web_App.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

// CORS Configuration 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

// Scoped Services
builder.Services.AddScoped<CommonMethods>(); 
builder.Services.AddScoped<PropertyService>(); 
builder.Services.AddScoped<SpaceService>(); 

// Interfaces and Implementations
builder.Services.AddScoped<IPropertyInterface, PropertyRepository>(); 
builder.Services.AddScoped<ISpaceInterface, SpaceRepository>(); 

// Database Context 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlcon")));

// Swagger 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware 
app.UseHttpsRedirection(); 
app.UseAuthorization();

//Use CORS 
app.UseCors("AllowAll");


app.MapControllers();
app.Run();
