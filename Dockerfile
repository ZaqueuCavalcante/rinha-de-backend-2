FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ./API ./
RUN apt-get update
RUN apt-get install --no-install-recommends -y clang zlib1g-dev
RUN dotnet restore
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["./API"]
