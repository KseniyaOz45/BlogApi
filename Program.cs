using BlogApi.Data;
using BlogApi.Middleware;
using Microsoft.EntityFrameworkCore;
using Slugify;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ���������� EF
builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// ���������� SlugHelper
builder.Services.AddSingleton<SlugHelper>();

// ����������� + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ��������� ����������� ������(��� ��������)
app.UseStaticFiles();

// ����������� (���� �� ����������, �� ����� �����)
app.UseAuthorization();

// ���������� ��������� ������
app.UseErrorHandling();

// ����������� ������������
app.MapControllers();

app.Run();

