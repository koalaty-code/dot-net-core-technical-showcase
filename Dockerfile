FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /source

COPY *.sln ./
COPY TechnicalShowcase/*.csproj ./TechnicalShowcase/
COPY TechnicalShowcase.Data/*.csproj ./TechnicalShowcase.Data/
COPY TechnicalShowcase.Services/*.csproj ./TechnicalShowcase.Services/
COPY TechnicalShowcase.Tests/*.csproj ./TechnicalShowcase.Tests/
RUN dotnet restore

COPY . ./

RUN dotnet test ./TechnicalShowcase.Tests/TechnicalShowcase.Tests.csproj

RUN dotnet publish -c Release -o /app

FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app .
ENTRYPOINT ["dotnet", "TechnicalShowcase.dll"]