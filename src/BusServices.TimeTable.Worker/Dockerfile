#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["BusServices.TimeTable.WorkerService/BusServices.TimeTable.WorkerService.csproj", "BusServices.TimeTable.WorkerService/"]
RUN dotnet restore "BusServices.TimeTable.WorkerService/BusServices.TimeTable.WorkerService.csproj"
COPY . .
WORKDIR "/src/BusServices.TimeTable.WorkerService"
RUN dotnet build "BusServices.TimeTable.WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BusServices.TimeTable.WorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BusServices.TimeTable.WorkerService.dll"]