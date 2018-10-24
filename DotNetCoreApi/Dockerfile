FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY DotNetCoreApi/DotNetCoreApi.csproj DotNetCoreApi/
RUN dotnet restore DotNetCoreApi/DotNetCoreApi.csproj
COPY . .
WORKDIR /src/DotNetCoreApi
RUN dotnet build DotNetCoreApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish DotNetCoreApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DotNetCoreApi.dll"]
