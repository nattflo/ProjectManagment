# ProjectManagment
Это тестовое задание.
# Как запустить
- Заменить в appsetting строку подключения к базе данных на свою. Мигрировать с помощью команды в консоли диспетчера пакетов
```powershell
dotnet ef database update --project ProjectManagment.DAL --startup-project ProjectManagment.WEB
```
# Немного о проекте
- Используется N-layer архитектура
- Есть unit-тесты для уровня бизнес логики (BLL)
- Написано на ASP.NET Core MVC
- База данных - MSSQL
# Какие библиотеки используются
Для фрондентда:
- Bootstrap
- JQuery
- Select2 - для удобной работы с динимаческими select
- DataTables - для функционала фильтрации, поиска, пагинации

Для backend:
- XUnit - для unit-тестов
- Moq - для мокирования объектов
- Mapper - для приведение viewModel в обычные модели уровня доступа к данным (DAL) и наоборот
- Entity Framework Core - ORM
