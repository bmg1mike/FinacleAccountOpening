#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["StanbicIBTC.AccountOpening.BulkAccountOpeningService/StanbicIBTC.AccountOpening.BulkAccountOpeningService.csproj", "StanbicIBTC.AccountOpening.BulkAccountOpeningService/"]
COPY ["StanbicIBTC.AccountOpening.Domain/StanbicIBTC.AccountOpening.Domain.csproj", "StanbicIBTC.AccountOpening.Domain/"]
COPY ["StanbicIBTC.AccountOpening.Data/StanbicIBTC.AccountOpening.Data.csproj", "StanbicIBTC.AccountOpening.Data/"]
COPY ["StanbicIBTC.AccountOpening.Service/StanbicIBTC.AccountOpening.Service.csproj", "StanbicIBTC.AccountOpening.Service/"]
RUN dotnet restore "StanbicIBTC.AccountOpening.BulkAccountOpeningService/StanbicIBTC.AccountOpening.BulkAccountOpeningService.csproj"
COPY . .
WORKDIR "/src/StanbicIBTC.AccountOpening.BulkAccountOpeningService"
RUN dotnet build "StanbicIBTC.AccountOpening.BulkAccountOpeningService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StanbicIBTC.AccountOpening.BulkAccountOpeningService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StanbicIBTC.AccountOpening.BulkAccountOpeningService.dll"]