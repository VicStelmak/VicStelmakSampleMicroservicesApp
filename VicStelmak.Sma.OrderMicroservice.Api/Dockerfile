#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["VicStelmak.Sma.OrderMicroservice.Api/VicStelmak.Sma.OrderMicroservice.Api.csproj", "VicStelmak.Sma.OrderMicroservice.Api/"]
COPY ["VicStelmak.Sma.OrderMicroservice.ApiDataLibrary/VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.csproj", "VicStelmak.Sma.OrderMicroservice.ApiDataLibrary/"]
COPY ./NuGet.Config .
RUN dotnet restore "./VicStelmak.Sma.OrderMicroservice.Api/./VicStelmak.Sma.OrderMicroservice.Api.csproj"
COPY . .
WORKDIR "/src/VicStelmak.Sma.OrderMicroservice.Api"
RUN dotnet build "./VicStelmak.Sma.OrderMicroservice.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./VicStelmak.Sma.OrderMicroservice.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VicStelmak.Sma.OrderMicroservice.Api.dll"]