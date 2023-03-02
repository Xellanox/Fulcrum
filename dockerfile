FROM mcr.microsoft.com/dotnet/core/sdk:8.0.100-preview.1-alpine3.17-amd64 as build-env
WORKDIR /app

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:8.0.0-preview.1-alpine3.17-amd64

ENV ASPNETCORE_URLS=http://*:5000
ENV ASPNETCORE_ENVIRONMENT=”production”

EXPOSE 5000

WORKDIR /app
COPY --from-build-env /App/out .
ENTRYPOINT ["dotnet", "FulcrumServer.dll"]