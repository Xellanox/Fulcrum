#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /src
COPY ["FulcrumServer/FulcrumServer.csproj", "FulcrumServer/"]
COPY ["libGatekeeper/libGatekeeper.csproj", "libGatekeeper/"]
COPY ["libFulcrum/libFulcrum.csproj", "libFulcrum/"]
RUN dotnet restore "FulcrumServer/FulcrumServer.csproj"
COPY . .
WORKDIR "/src/FulcrumServer"
RUN dotnet build "FulcrumServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FulcrumServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FulcrumServer.dll"]