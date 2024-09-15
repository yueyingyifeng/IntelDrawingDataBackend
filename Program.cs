using IntelDrawingDataBackend.Controllers;
using IntelDrawingDataBackend.DB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//----------跨域问题
string[] urls = new[] { "http://localhost:5173" };

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(builder => builder.WithOrigins(urls).AllowAnyMethod().AllowAnyHeader().AllowCredentials())
);

//----------Session 启用
// 添加 DataProtection 服务 (用于 session 加密)
builder.Services.AddDataProtection();
builder.Services.AddDistributedMemoryCache();
// 添加 Session 服务
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session 过期时间
    options.Cookie.HttpOnly = true;                 // 确保 cookie 只能通过 HTTP 访问
    options.Cookie.IsEssential = true;              // 标记为必要的 cookie
});
//----------------

var app = builder.Build();


app.UseCors();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSession();

app.MapControllers();

DBManager.InitDB();

app.Run();

