FROM mcr.microsoft.com/dotnet/runtime:6.0-focal AS base
WORKDIR /app

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["StanbicIBTC.AccountOpening.BulkAccountOpeningService/StanbicIBTC.AccountOpening.BulkAccountOpeningService.csproj", "StanbicIBTC.AccountOpening.BulkAccountOpeningService/"]
RUN dotnet restore "StanbicIBTC.AccountOpening.BulkAccountOpeningService/StanbicIBTC.AccountOpening.BulkAccountOpeningService.csproj"
COPY . .
WORKDIR "/src/StanbicIBTC.AccountOpening.BulkAccountOpeningService"
RUN dotnet build "StanbicIBTC.AccountOpening.BulkAccountOpeningService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StanbicIBTC.AccountOpening.BulkAccountOpeningService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StanbicIBTC.AccountOpening.BulkAccountOpeningService.dll"]
