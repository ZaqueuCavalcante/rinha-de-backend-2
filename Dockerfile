FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "./API/API.csproj"
RUN dotnet publish "./API/API.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 3000

ENTRYPOINT [ "dotnet", "API.dll" ]
