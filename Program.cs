using IntelDrawingDataBackend.Controllers;
using IntelDrawingDataBackend.DB;
using log4net.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//----------��������
string[] urls = new[] { "http://localhost:5173", "http://120.24.177.154" };


builder.Services.AddCors(options =>
    options.AddDefaultPolicy(builder => builder.WithOrigins(urls)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
    )
);

//----------Session ����
// ��� DataProtection ���� (���� session ����)
builder.Services.AddDataProtection();
builder.Services.AddDistributedMemoryCache();
// ��� Session ����
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session ����ʱ��
    options.Cookie.HttpOnly = true;                 // ȷ�� cookie ֻ��ͨ�� HTTP ����
    options.Cookie.IsEssential = true;              // ���Ϊ��Ҫ�� cookie
});
//----------------

// ���� log4net ����
XmlConfigurator.Configure(new System.IO.FileInfo("Config/log4net.config"));

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
