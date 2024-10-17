#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["VicStelmak.Sma.ProductMicroservice.Api/VicStelmak.Sma.ProductMicroservice.Api.csproj", "VicStelmak.Sma.ProductMicroservice.Api/"]
COPY ["VicStelmak.Sma.ProductMicroservice.Infrastructure/VicStelmak.Sma.ProductMicroservice.Infrastructure.csproj", "VicStelmak.Sma.ProductMicroservice.Infrastructure/"]
COPY ["VicStelmak.Sma.ProductMicroservice.Application/VicStelmak.Sma.ProductMicroservice.Application.csproj", "VicStelmak.Sma.ProductMicroservice.Application/"]
COPY ["VicStelmak.Sma.ProductMicroservice.Domain/VicStelmak.Sma.ProductMicroservice.Domain.csproj", "VicStelmak.Sma.ProductMicroservice.Domain/"]
COPY ./NuGet.Config .
RUN dotnet restore "./VicStelmak.Sma.ProductMicroservice.Api/./VicStelmak.Sma.ProductMicroservice.Api.csproj"
COPY . .
WORKDIR "/src/VicStelmak.Sma.ProductMicroservice.Api"
RUN dotnet build "./VicStelmak.Sma.ProductMicroservice.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./VicStelmak.Sma.ProductMicroservice.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VicStelmak.Sma.ProductMicroservice.Api.dll"]