
using BookStore.DataAccess.Repository;
using BookStore.Infrastructure.Configuration;
using BookTour.Application.Interface;
using BookTour.Application.Service;
using BookTour.Domain.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// register db
builder.Services.RegisterDB(builder.Configuration);
// register api
builder.Services.ConfigApi();
// register DI
builder.Services.RegisterDI();


builder.Logging.ClearProviders();
builder.Logging.AddConsole();


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