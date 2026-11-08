FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY QueueContracts/QueueContracts.csproj QueueContracts/
COPY BookingWorker/BookingWorker.csproj BookingWorker/
RUN dotnet restore BookingWorker/BookingWorker.csproj
COPY QueueContracts/ QueueContracts/
COPY BookingWorker/ BookingWorker/
RUN dotnet publish BookingWorker/BookingWorker.csproj -c Release -o /app /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "BookingWorker.dll"]
