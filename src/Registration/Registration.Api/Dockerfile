#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Registration/Registration.Api/Registration.Api.csproj", "src/Registration/Registration.Api/"]
COPY ["src/Registration/Registration.Application/Registration.Application.csproj", "src/Registration/Registration.Application/"]
COPY ["src/Registration/Registration.Domain/Registration.Domain.csproj", "src/Registration/Registration.Domain/"]
COPY ["src/Registration/Registration.Inftastructure/Registration.Inftastructure.csproj", "src/Registration/Registration.Inftastructure/"]
RUN dotnet restore "./src/Registration/Registration.Api/Registration.Api.csproj"
COPY . .
WORKDIR "/src/src/Registration/Registration.Api"
RUN dotnet build "./Registration.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Registration.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Registration.Api.dll"]