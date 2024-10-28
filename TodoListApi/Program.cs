using Microsoft.EntityFrameworkCore;
using TodoListApi.auth;
using TodoListApi.Data;
using TodoListApi.Interface;
using TodoListApi.Repository;
using Microsoft.OpenApi.Models; // Swagger için gerekli

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskReposýtory, TaskReposýtory>();
builder.Services.AddScoped<AuthService>();

// Swagger ayarlarý
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoListApi", Version = "v1" });

    // Basic Authentication tanýmýný ekleyin
    c.AddSecurityDefinition("basicAuth", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Scheme = "basic",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Description = "Basic Authentication using username and password."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "basicAuth" } }, new string[] { } }
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

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
