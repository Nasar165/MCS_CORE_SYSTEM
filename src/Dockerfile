#Docker support has not been implemented yet do not run.
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS build
WORKDIR /app
EXPOSE 80 443
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "api.dll" ]

