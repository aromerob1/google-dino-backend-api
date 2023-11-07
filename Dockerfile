FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["google-dino-backend.csproj", "./"]
RUN dotnet restore "google-dino-backend.csproj"
COPY . .
RUN dotnet build "google-dino-backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "google-dino-backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "google-dino-backend.dll"]
