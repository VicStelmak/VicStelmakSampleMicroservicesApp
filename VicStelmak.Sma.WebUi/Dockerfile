FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["VicStelmak.Sma.WebUi/VicStelmak.Sma.WebUi.csproj", "VicStelmak.Sma.WebUi/"]
COPY ["VicStelmak.Sma.WebUiDataLibrary/VicStelmak.Sma.WebUiDataLibrary.csproj", "VicStelmak.Sma.WebUiDataLibrary/"]
RUN dotnet restore VicStelmak.Sma.WebUi/VicStelmak.Sma.WebUi.csproj 
COPY . .
RUN dotnet build VicStelmak.Sma.WebUi/VicStelmak.Sma.WebUi.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish VicStelmak.Sma.WebUi/VicStelmak.Sma.WebUi.csproj -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY VicStelmak.Sma.WebUi/nginx.conf /etc/nginx/nginx.conf