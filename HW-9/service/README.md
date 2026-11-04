# Org.OpenAPITools - ASP.NET Core 10.0 Server

API бронирования переговорок в офисе.

Кто вызывает метод, сервер узнаёт из JWT. Имя и идентификатор пользователя
в параметрах и в теле не передаются.

401 — пользователя установить не удалось (нет токена, токен повреждён или просрочен).
403 — пользователь известен, но это действие ему запрещено.

Наружу выходят только публичные DTO из `components.schemas`.
Список комнаты, карточка комнаты, строка календаря и бронь — разные схемы.

Бронь занимает полуинтервал `[startsAt, endsAt)`: стык встык конфликтом не считается.


## Upgrade NuGet Packages

NuGet packages get frequently updated.

To upgrade this solution to the latest version of all NuGet packages, use the dotnet-outdated tool.


Install dotnet-outdated tool:

```
dotnet tool install --global dotnet-outdated-tool
```

Upgrade only to new minor versions of packages

```
dotnet outdated --upgrade --version-lock Major
```

Upgrade to all new versions of packages (more likely to include breaking API changes)

```
dotnet outdated --upgrade
```


## Run

Linux/OS X:

```
sh build.sh
```

Windows:

```
build.bat
```
## Run in Docker

```
cd src/Org.OpenAPITools
docker build -t org.openapitools .
docker run -p 5000:8080 org.openapitools
```
