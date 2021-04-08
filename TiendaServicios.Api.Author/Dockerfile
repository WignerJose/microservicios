#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["TiendaServicios.Api.Author/TiendaServicios.Api.Author.csproj", "TiendaServicios.Api.Author/"]
RUN dotnet restore "TiendaServicios.Api.Author/TiendaServicios.Api.Author.csproj"
COPY . .
WORKDIR "/src/TiendaServicios.Api.Author"
RUN dotnet build "TiendaServicios.Api.Author.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TiendaServicios.Api.Author.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TiendaServicios.Api.Author.dll"]
