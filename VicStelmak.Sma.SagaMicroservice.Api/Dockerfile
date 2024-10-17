#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["VicStelmak.Sma.SagaMicroservice.Api/VicStelmak.Sma.SagaMicroservice.Api.csproj", "VicStelmak.Sma.SagaMicroservice.Api/"]
COPY ["VicStelmak.Sma.SagaMicroservice.ApiDataLibrary/VicStelmak.Sma.SagaMicroservice.ApiDataLibrary.csproj", "VicStelmak.Sma.SagaMicroservice.ApiDataLibrary/"]
COPY ./NuGet.Config .
RUN dotnet restore "./VicStelmak.Sma.SagaMicroservice.Api/./VicStelmak.Sma.SagaMicroservice.Api.csproj"
COPY . .
WORKDIR "/src/VicStelmak.Sma.SagaMicroservice.Api"
RUN dotnet build "./VicStelmak.Sma.SagaMicroservice.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./VicStelmak.Sma.SagaMicroservice.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VicStelmak.Sma.SagaMicroservice.Api.dll"]