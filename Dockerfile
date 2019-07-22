FROM mcr.microsoft.com/dotnet/core/sdk:2.2.301-alpine3.9 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet restore SingleSignOn/SingleSignOn.sln
RUN dotnet publish SingleSignOn/SingleSignOn.sln -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2.6-alpine3.9
LABEL version="1.1" maintainer="otaviolarrosa"
WORKDIR /app

COPY --from=build-env /app/out .
RUN ls
ENTRYPOINT ["dotnet", "SingleSignOn.dll"]
