#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["src/Managerment.HttpApi.Host/Managerment.HttpApi.Host.csproj", "src/Managerment.HttpApi.Host/"]
COPY ["src/Managerment.EntityFrameworkCore/Managerment.EntityFrameworkCore.csproj", "src/Managerment.EntityFrameworkCore/"]
COPY ["src/Managerment.Domain/Managerment.Domain.csproj", "src/Managerment.Domain/"]
COPY ["src/Managerment.Domain.Shared/Managerment.Domain.Shared.csproj", "src/Managerment.Domain.Shared/"]
COPY ["src/Managerment.HttpApi/Managerment.HttpApi.csproj", "src/Managerment.HttpApi/"]
COPY ["src/Managerment.Application/Managerment.Application.csproj", "src/Managerment.Application/"]
COPY ["src/Managerment.Application.Contracts/Managerment.Application.Contracts.csproj", "src/Managerment.Application.Contracts/"]
RUN dotnet restore "src/Managerment.HttpApi.Host/Managerment.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/src/Managerment.HttpApi.Host"
RUN dotnet build "Managerment.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Managerment.HttpApi.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Managerment.HttpApi.Host.dll"]