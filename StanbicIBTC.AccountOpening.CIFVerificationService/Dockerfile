#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["StanbicIBTC.AccountOpening.CIFVerificationService/StanbicIBTC.AccountOpening.CIFVerificationService.csproj", "StanbicIBTC.AccountOpening.CIFVerificationService/"]
RUN dotnet restore "StanbicIBTC.AccountOpening.CIFVerificationService/StanbicIBTC.AccountOpening.CIFVerificationService.csproj"
COPY . .
WORKDIR "/src/StanbicIBTC.AccountOpening.CIFVerificationService"
RUN dotnet build "StanbicIBTC.AccountOpening.CIFVerificationService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StanbicIBTC.AccountOpening.CIFVerificationService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StanbicIBTC.AccountOpening.CIFVerificationService.dll"]