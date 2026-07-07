FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DataPoints.Api/DataPoints.Api.csproj", "DataPoints.Api/"]
COPY ["DataPoints.Application/DataPoints.Application.csproj", "DataPoints.Application/"]
COPY ["DataPoints.Contract/DataPoints.Contract.csproj", "DataPoints.Contract/"]
COPY ["DataPoints.Crosscutting/DataPoints.Crosscutting.csproj", "DataPoints.Crosscutting/"]
COPY ["DataPoints.Crosscutting.Mapper/DataPoints.Crosscutting.Mapper.csproj", "DataPoints.Crosscutting.Mapper/"]
COPY ["DataPoints.Domain/DataPoints.Domain.csproj", "DataPoints.Domain/"]
COPY ["DataPoints.Infrastructure/DataPoints.Infrastructure.csproj", "DataPoints.Infrastructure/"]
COPY ["DataPoints.Presentation/DataPoints.Presentation.csproj", "DataPoints.Presentation/"]
RUN dotnet restore "DataPoints.Api/DataPoints.Api.csproj"
COPY . .
WORKDIR "/src/DataPoints.Api"
RUN dotnet build "DataPoints.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DataPoints.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataPoints.Api.dll"]
