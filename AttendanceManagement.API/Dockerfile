# Base image Linux cho môi trường Production (Render, Railway, Fly.io, VPS Docker...)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
ENV TZ=Asia/Ho_Chi_Minh
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

# Stage build project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AttendanceManagement.API/AttendanceManagement.API.csproj", "AttendanceManagement.API/"]
RUN dotnet restore "./AttendanceManagement.API/AttendanceManagement.API.csproj"
COPY . .
WORKDIR "/src/AttendanceManagement.API"
RUN dotnet build "./AttendanceManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AttendanceManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AttendanceManagement.API.dll"]
