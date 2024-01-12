using QlhsServer.middleware;
using QlhsServer.Repositories;
using QlhsServer.DependencyInjection;
using QlhsServer.Models.Response;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllerConfig();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddIdentityConfig();
builder.Services.AddEFconfiguration(configuration);
builder.Services.AddAutoMapper(typeof(Program));

// Life cycle DI: AddSingleton(), AddTransient(), AddScoped()
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFileRepository, FileStorageRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddAuthenticationConfig(configuration);
builder.Services.AddRedisConfiguration(configuration);

var app = builder.Build();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseStaticFiles();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
