using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PadelBackend.Services;
using Microsoft.EntityFrameworkCore;
using PadelBackend.Repositories;
using AutoMapper;
using PadelBackend.Config;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options => 
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "An ASP.NET Core Web API for managing todos",
    });
    options.AddSecurityDefinition("Token", new OpenApiSecurityScheme()
    {
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Name = "Authorization",
        Scheme = "bearer"
    });
    options.OperationFilter<AuthOperationsFilter>();
});

// secret key
var secretKey = builder.Configuration.GetSection("JwtSettings").GetSection("SecretKey").ToString();


// JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });

// Services

builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<IUsersServices, UserServices>();
builder.Services.AddScoped<IEncoderService, EncoderService>();

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection"));
});

// Mapper
builder.Services.AddAutoMapper(typeof(Mapping));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
