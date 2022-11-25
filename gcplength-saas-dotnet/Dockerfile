#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["gcplength-saas-dotnet/gcplength-saas-dotnet.csproj", "gcplength-saas-dotnet/"]
RUN dotnet restore "gcplength-saas-dotnet/gcplength-saas-dotnet.csproj"
COPY . .
WORKDIR "/src/gcplength-saas-dotnet"
RUN dotnet build "gcplength-saas-dotnet.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "gcplength-saas-dotnet.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "gcplength-saas-dotnet.dll"]