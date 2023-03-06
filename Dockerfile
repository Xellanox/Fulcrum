#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /src
COPY ["Fulcrum.csproj", "."]
RUN dotnet restore "./Fulcrum.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Fulcrum.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fulcrum.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fulcrum.dll"]