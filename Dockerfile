FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DeliciasAPI.csproj", "."]
RUN dotnet restore "./DeliciasAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "DeliciasAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeliciasAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliciasAPI.dll"]