# КТ12 — Система управления профилями пользователей (ASP.NET Core MVC + EF Core)

Проект: **UserProfilesApp** — веб‑приложение на ASP.NET Core MVC с Entity Framework Core (SQLite).

Задание: создать систему управления пользователями и их профилями, где:
- **у каждого пользователя ровно один профиль**
- **у каждого профиля ровно один пользователь**
- реализованы **CRUD** операции и **пользовательский интерфейс**
- настроены **атрибуты + Fluent API**
- настроено **каскадное удаление** в связи 1–1

## Реализация требований

### 1) Модели данных
- `User`
- `UserProfile`

Связь 1–1 настроена через **PK=FK**: `UserProfile.UserId` — первичный ключ и внешний ключ на `User.Id`.

### 2) База данных и конфигурация
- SQLite база: `UserProfilesApp/userprofiles.db`
- строка подключения: `UserProfilesApp/appsettings.json` → `ConnectionStrings:DefaultConnection`
- Fluent API:
  - 1–1 связь `User` ↔ `UserProfile`
  - каскадное удаление `OnDelete(DeleteBehavior.Cascade)`
  - уникальные индексы на `User.UserName` и `User.Email`

### 3) CRUD + UI
CRUD реализован в `UsersController` и Razor‑страницах:
- `Views/Users/Index` — список
- `Views/Users/Details` — детали
- `Views/Users/Create` — создание пользователя + профиля
- `Views/Users/Edit` — редактирование пользователя + профиля
- `Views/Users/Delete` — удаление (профиль удаляется каскадно)

В верхнем меню добавлен раздел **Users**.

## Как запустить (PowerShell)

Перейдите в папку задания:

```powershell
cd "C:\Users\User\Desktop\ADO DOTNET\КТ12"
```

Запуск приложения:

```powershell
cd "UserProfilesApp"
dotnet run
```

Ожидаемый вывод (примерно):
- `Now listening on: http://localhost:5171` (порт может отличаться)
- `Application started. Press Ctrl+C to shut down.`

Откройте в браузере адрес из строки `Now listening on ...` и перейдите в **Users**.

## Миграции EF Core (если нужно пересоздать БД)

```powershell
cd "C:\Users\User\Desktop\ADO DOTNET\КТ12\UserProfilesApp"
dotnet tool run dotnet-ef migrations add InitialCreate
dotnet tool run dotnet-ef database update
```

## Структура проекта (основные файлы)
- `UserProfilesApp/Models/User.cs`
- `UserProfilesApp/Models/UserProfile.cs`
- `UserProfilesApp/Data/AppDbContext.cs`
- `UserProfilesApp/Controllers/UsersController.cs`
- `UserProfilesApp/Views/Users/*.cshtml`

