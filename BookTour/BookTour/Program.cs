
using BookStore.DataAccess.Repository;
using BookStore.Infrastructure.Configuration;
using BookTour.Application.Dto;
using BookTour.Application.Interface;
using BookTour.Application.Service;
using BookTour.Domain.Interface;
using Microsoft.AspNetCore.Authentication.Google;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// register db
builder.Services.RegisterDB(builder.Configuration);
builder.Services.RegisterDI();
builder.Services.ConfigApi();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

//authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie() // Cấu hình cookie authentication
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["OAuth:Google:ClientId"];
    options.ClientSecret = builder.Configuration["OAuth:Google:ClientSecret"];
    options.Scope.Add("email");
    options.Scope.Add("profile");
    options.SaveTokens = true; // Lưu token để có thể sử dụng
})
.AddFacebook(options =>
{
    options.AppId = builder.Configuration["OAuth:Facebook:ClientId"];
    options.AppSecret = builder.Configuration["OAuth:Facebook:ClientSecret"];
    options.Scope.Add("email");
    options.Scope.Add("public_profile");
    options.SaveTokens = true;
});


var app = builder.Build();

app.UseCors("AllowSpecificOrigin");
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