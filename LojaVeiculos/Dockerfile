#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LojaVeiculos.csproj", "."]
RUN dotnet restore "LojaVeiculos.csproj"
COPY . .
WORKDIR "/src/"
RUN apt-get update && apt-get install -y nodejs
RUN dotnet build "LojaVeiculos.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "LojaVeiculos.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LojaVeiculos.dll"]
