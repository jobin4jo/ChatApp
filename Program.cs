using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using ChatApp.DB_Context;
using Microsoft.EntityFrameworkCore;
using ChatApp.IRepositories;
using ChatApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ChatApp.IService;
using ChatApp.Service;
using ChatApp.ChatHUB;
using System.Net;
using ChatApp.Common.CommonDTO;
using DotNetEnv;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DB Connectivity Configurations

builder.Services.AddDbContext<ChatDBContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

Env.Load();

///mapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.Configure<PayUSettings>(builder.Configuration.GetSection("PayUSettings"));
///cors configuration:
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                                .AllowAnyHeader().SetIsOriginAllowed((host) => true)
                                .AllowAnyMethod().AllowCredentials();
        });
});

///Dependency Configuration:
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IPaymentServices, PaymentServices>();

builder.Services.Configure<PayUSettings>(builder.Configuration.GetSection("PayUSettings"));
//signalR configuration:
builder.Services.AddSignalR();

//Authentification
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("User", policy => policy.RequireRole("User"));
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();

app.MapHub<MessageHub>("/userHub");
//app.UseSignalR(routes =>
//{
//    routes.MapHub<General>("/hubs/general");
//});
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.UseStaticFiles();
app.MapControllers();

app.Run();
