FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /src
COPY MinIOService.sln ./
COPY MinIOService/*.csproj MinIOService/
COPY MinIOService.Domain/*.csproj MinIOService.Domain/
COPY MinIOService.Infrastructure/*.csproj MinIOService.Infrastructure/
RUN dotnet restore
COPY . ./

RUN dotnet build "/src/MinIOService.Domain/MinIOService.Domain.csproj" -c Release -o /app
RUN dotnet build "/src/MinIOService.Infrastructure/MinIOService.Infrastructure.csproj" -c Release -o /app
RUN dotnet build "/src/MinIOService/MinIOService.csproj" -c Release -o /app

FROM build-env AS publish
RUN dotnet publish "/src/MinIOService/MinIOService.csproj" -c Release -o /app 

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=publish /app .
EXPOSE 8100 8101
ENTRYPOINT ["dotnet", "MinIOService.dll"]