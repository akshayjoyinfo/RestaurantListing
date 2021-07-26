FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR /app

COPY src /app/src
COPY tests /app/tests

WORKDIR /app/src/InHouse

RUN dotnet restore


WORKDIR /app/src/InHouse/InHouse.API


RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app/src/InHouse/InHouse.API
COPY --from=build-env /app/src/InHouse/InHouse.API/out .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet InHouse.API.dll
