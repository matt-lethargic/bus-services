#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["BusServices.Buses.Api/BusServices.Buses.Api.csproj", "BusServices.Buses.Api/"]
COPY ["BusServices.Swagger/BusServices.Api.csproj", "BusServices.Swagger/"]
COPY ["BusServices.NServiceBus/BusServices.NServiceBus.csproj", "BusServices.NServiceBus/"]
COPY ["BusServices.Buses.Application/BusServices.Buses.Application.csproj", "BusServices.Buses.Application/"]
COPY ["BusServices.Buses.Domain/BusServices.Buses.Domain.csproj", "BusServices.Buses.Domain/"]
COPY ["BusServices.Messages/BusServices.Messages.csproj", "BusServices.Messages/"]
COPY ["BusServices.Buses.Persistence.InMemory/BusServices.Buses.Persistence.InMemory.csproj", "BusServices.Buses.Persistence.InMemory/"]
COPY ["BusServices.Buses.EventPublisher.NServiceBus/BusServices.Buses.EventPublisher.NServiceBus.csproj", "BusServices.Buses.EventPublisher.NServiceBus/"]
RUN dotnet restore "BusServices.Buses.Api/BusServices.Buses.Api.csproj"
COPY . .
WORKDIR "/src/BusServices.Buses.Api"
RUN dotnet build "BusServices.Buses.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BusServices.Buses.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BusServices.Buses.Api.dll"]
