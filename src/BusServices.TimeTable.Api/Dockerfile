#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["BusServices.TimeTable.Api/BusServices.TimeTable.Api.csproj", "BusServices.TimeTable.Api/"]
RUN dotnet restore "BusServices.TimeTable.Api/BusServices.TimeTable.Api.csproj"
COPY . .
WORKDIR "/src/BusServices.TimeTable.Api"
RUN dotnet build "BusServices.TimeTable.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BusServices.TimeTable.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BusServices.TimeTable.Api.dll"]