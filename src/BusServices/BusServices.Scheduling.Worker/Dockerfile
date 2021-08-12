#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["BusServices.Scheduling.Worker/BusServices.Scheduling.Worker.csproj", "BusServices.Scheduling.Worker/"]
COPY ["BusServices.NServiceBus/BusServices.NServiceBus.csproj", "BusServices.NServiceBus/"]
COPY ["BusServices.Messages/BusServices.Messages.csproj", "BusServices.Messages/"]
COPY ["BusServices.Scheduling.Persistence.InMemory/BusServices.Scheduling.Persistence.InMemory.csproj", "BusServices.Scheduling.Persistence.InMemory/"]
COPY ["BusServices.Scheduling.Domain/BusServices.Scheduling.Domain.csproj", "BusServices.Scheduling.Domain/"]
COPY ["BusServices.Scheduling.Application/BusServices.Scheduling.Application.csproj", "BusServices.Scheduling.Application/"]
RUN dotnet restore "BusServices.Scheduling.Worker/BusServices.Scheduling.Worker.csproj"
COPY . .
WORKDIR "/src/BusServices.Scheduling.Worker"
RUN dotnet build "BusServices.Scheduling.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BusServices.Scheduling.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BusServices.Scheduling.Worker.dll"]