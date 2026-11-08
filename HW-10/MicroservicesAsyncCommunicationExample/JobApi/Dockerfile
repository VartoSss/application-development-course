FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY QueueContracts/QueueContracts.csproj QueueContracts/
COPY JobApi/JobApi.csproj JobApi/
RUN dotnet restore JobApi/JobApi.csproj
COPY QueueContracts/ QueueContracts/
COPY JobApi/ JobApi/
RUN dotnet publish JobApi/JobApi.csproj -c Release -o /app /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "JobApi.dll"]
