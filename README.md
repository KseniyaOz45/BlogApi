📖 Blog API
Blog API — это учебный проект на ASP.NET Core Web API, представляющий собой серверную часть блога.
Проект создан как переходный этап между простыми CRUD-приложениями и более сложными системами (например, интернет-магазином).

🚀 Функциональность
Управление пользователями (регистрация, авторизация)

Управление категориями

Управление постами

Управление комментариями

Управление тегами и фильтрация постов по тегам

Авторизация и аутентификация (JWT)

Использование DTO и AutoMapper

Репозиторная архитектура

Сервисный слой для бизнес-логики

🛠️ Стек технологий
ASP.NET Core 8 Web API

Entity Framework Core

AutoMapper

DTO для обмена данными

JWT для авторизации

SQL Server

📂 Архитектура
BlogAPI/
│-- Controllers/      → REST API контроллеры  
│-- DTOs/             → Data Transfer Objects  
│-- Entities/         → Сущности базы данных  
│-- Repositories/     → Репозитории для работы с БД  
│-- Services/         → Бизнес-логика  
│-- Mappings/         → Профили AutoMapper  
│-- Program.cs        → Точка входа
│-- appsettings.json  → Конфигурация проекта  
⚙️ Требования
.NET 8 SDK

SQL Server

Visual Studio / Rider / VS Code

▶️ Запуск проекта
Клонировать репозиторий:
git clone https://github.com/username/blog-api.git
cd blog-api
Настроить подключение к БД в appsettings.json

Применить миграции:
dotnet ef database update

Запустить приложение:
dotnet run

После запуска API будет доступно по адресу:
👉 https://localhost:5001/swagger (Swagger UI)
