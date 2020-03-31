FROM mcr.microsoft.com/dotnet/core/sdk:3.1.201-alpine3.11 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet restore SingleSignOn.sln
RUN dotnet publish SingleSignOn.sln -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.3-alpine3.11
LABEL version="1.1" maintainer="otaviolarrosa"
WORKDIR /app

COPY --from=build-env /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "SingleSignOn.dll"]
