using BlogApi.Middleware;
using Slugify;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Подключаем SlugHelper
builder.Services.AddSingleton<SlugHelper>();

// Контроллеры + Swagger
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

// Поддержка статических файлов(для картинок)
app.UseStaticFiles();

// Авторизация (пока не используем, но пусть стоит)
app.UseAuthorization();

// Глобальная обработка ошибок
app.UseErrorHandling();

// Подключение контроллеров
app.MapControllers();

app.Run();

