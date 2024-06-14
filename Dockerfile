FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DeliciasAPI.csproj.user", "."]
RUN dotnet restore "./DeliciasAPI.csproj.user"
COPY . .
WORKDIR "/src/."
RUN dotnet build "DeliciasAPI.csproj.user" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeliciasAPI.csproj.user" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliciasAPI.dll"]