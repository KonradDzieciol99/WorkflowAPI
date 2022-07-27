using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WorkflowApi;
using WorkflowApi.Data;
using WorkflowApi.DataTransferObject;
using WorkflowApi.Models;
using WorkflowApi.Models.Validators;
using WorkflowApi.Services;
using WorkflowApi.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var authenticationSettings = new AuthSettings();
builder.Configuration.GetSection("JWT").Bind(authenticationSettings);
//builder.Services
builder.Services.AddSingleton(authenticationSettings);


//identity
builder.Services.AddIdentityCore<AppUser>(opt =>
    {
        opt.Password.RequireNonAlphanumeric = false;
    })
    .AddRoles<AppRole>()
    .AddSignInManager<SignInManager<AppUser>>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            //ValidateIssuerSigningKey = true,
            //ValidateIssuerSigningKey = true
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

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = contextUnknow =>
            {
                var accessToken = contextUnknow.Request.Query["access_token"];
                var path = contextUnknow.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                {
                    contextUnknow.Token = accessToken;
                }
                return Task.CompletedTask;
            }
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
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IAppTaskService, AppTaskService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR(o =>
{
    o.EnableDetailedErrors=true;
});//signalR serv
builder.Services.AddCors();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "RedisDemo_";
});//


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();//CustomExceptionMiddleware

app.UseHttpsRedirection();

app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
});

app.UseAuthentication();//added

app.UseAuthorization();//a mo¿e to?

app.MapControllers();//dodajemy/mapujemy tutaj œcie¿ki(route) do kontrolerów czyli tak w³aœciwie sprawiamy ¿e np.
//'api/Messages/PostMessage' jest widoczna i dzia³a
app.MapHub<PresenceHub>("hubs/presence");//dodajemy tutaj œcie¿kê(route) do naszego haba odpowiadzialnego za aktywnoœæ u¿ytkpowników
//signal nie mo¿e wysy³aæ nag³ówków do autoryzacji

//jeœli nie bêdzie dzia³œ signalR dodaæ Cors

app.Run();
