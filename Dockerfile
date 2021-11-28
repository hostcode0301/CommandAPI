# restore
FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY *.sln .
COPY test/CommandAPI.Tests/*.csproj test/CommandAPI.Tests/ 
COPY src/CommandAPI/*.csproj src/CommandAPI/
RUN dotnet restore
COPY . .

# testing
FROM build as testing
WORKDIR /src/CommandAPI
RUN dotnet build
WORKDIR /src/CommandAPI.Tests
RUN dotnet test

# publish
FROM build AS publish
WORKDIR /src/CommandAPI
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:&PORT dotnet CommandAPI.dll
