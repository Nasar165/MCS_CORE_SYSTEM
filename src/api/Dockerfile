#Docker support has not been implemented yet do not run.
FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS build
WORKDIR /app
EXPOSE 8080
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", 'api.dll' ]