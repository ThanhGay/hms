# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HMS.WebAPI/HMS.WebAPI.csproj", "HMS.WebAPI/"]
COPY ["Services/Auth/HMS.Auth.ApplicationService/HMS.Auth.ApplicationService.csproj", "Services/Auth/HMS.Auth.ApplicationService/"]
COPY ["Services/Shared/HMS.Shared.ApplicationService/HMS.Shared.ApplicationService.csproj", "Services/Shared/HMS.Shared.ApplicationService/"]
COPY ["Services/Shared/HMS.Shared.Constant/HMS.Shared.Constant.csproj", "Services/Shared/HMS.Shared.Constant/"]
COPY ["Services/Auth/HMS.Auth.Domain/HMS.Auth.Domain.csproj", "Services/Auth/HMS.Auth.Domain/"]
COPY ["Services/Auth/HMS.Auth.Dtos/HMS.Auth.Dtos.csproj", "Services/Auth/HMS.Auth.Dtos/"]
COPY ["Services/Auth/HMS.Auth.Infrastructures/HMS.Auth.Infrastructures.csproj", "Services/Auth/HMS.Auth.Infrastructures/"]
COPY ["Services/Hotel/HMS.Hol.ApplicationService/HMS.Hol.ApplicationService.csproj", "Services/Hotel/HMS.Hol.ApplicationService/"]
COPY ["Services/Hotel/HMS.Hol.Dtos/HMS.Hol.Dtos.csproj", "Services/Hotel/HMS.Hol.Dtos/"]
COPY ["Services/Hotel/HMS.Hol.Infrastructures/HMS.Hol.Infrastructures.csproj", "Services/Hotel/HMS.Hol.Infrastructures/"]
COPY ["Services/Hotel/HMS.Hol.Domain/HMS.Hol.Domain.csproj", "Services/Hotel/HMS.Hol.Domain/"]
COPY ["Services/Notification/HMS.Noti.ApplicationService/HMS.Noti.ApplicationService.csproj", "Services/Notification/HMS.Noti.ApplicationService/"]
COPY ["Services/Notification/HMS.Noti.Domain/HMS.Noti.Domain.csproj", "Services/Notification/HMS.Noti.Domain/"]
COPY ["Services/Notification/HMS.Noti.Dtos/HMS.Noti.Dtos.csproj", "Services/Notification/HMS.Noti.Dtos/"]
COPY ["Services/Notification/HMS.Noti.Infrastructures/HMS.Noti.Infrastructures.csproj", "Services/Notification/HMS.Noti.Infrastructures/"]
RUN dotnet restore "./HMS.WebAPI/HMS.WebAPI.csproj"
COPY . .
WORKDIR "/src/HMS.WebAPI"
RUN dotnet build "./HMS.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HMS.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HMS.WebAPI.dll"]