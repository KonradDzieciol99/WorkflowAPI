using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WorkflowApi;
using WorkflowApi.Data;
using WorkflowApi.DataTransferObject;
using WorkflowApi.Models;
using WorkflowApi.Models.Validators;
using WorkflowApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var authenticationSettings = new AuthSettings();
builder.Configuration.GetSection("JWT").Bind(authenticationSettings);
//builder.Services
builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            //ValidateIssuerSigningKey = true,
            //ValidIssuer = builder.Configuration.GetSection("JWT:jwtIssuer").Value,
            //ValidAudience = builder.Configuration.GetSection("JWT:jwtIssuer").Value,
            //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])) 
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(authenticationSettings.JwtKey)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = authenticationSettings.jwtIssuer,
            ValidAudience = authenticationSettings.jwtIssuer
        };
    });
builder.Services.AddControllers().AddFluentValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection")
));
builder.Services.AddScoped<ErrorHandlingMiddleware> (); //CustomExceptionMiddleware doczytaæ czy potrzebny do tego servis
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator> ();
builder.Services.AddScoped<IAccountService,AccountService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IPasswordHasher<AppUser>, PasswordHasher<AppUser>>();
builder.Services.AddScoped<IPTaskService, PTaskService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();//CustomExceptionMiddleware

app.UseHttpsRedirection();

app.UseAuthentication();//added

app.UseAuthorization();//a mo¿e to?

app.MapControllers();

app.Run();
