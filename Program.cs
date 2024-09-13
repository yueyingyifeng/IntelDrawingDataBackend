using IntelDrawingDataBackend.Controllers;
using IntelDrawingDataBackend.DB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//----------¿çÓòÎÊÌâ
string[] urls = new[] { "http://localhost:5173" };

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(builder => builder.WithOrigins(urls).AllowAnyMethod().AllowAnyHeader().AllowCredentials())
);


var app = builder.Build();


app.UseCors();

//----------------

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

DBManager.InitDB();

app.Run();

