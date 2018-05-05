################################################################################
# STAGE I: BUILDER
FROM microsoft/dotnet:sdk AS builder

ENV HOME=/app NUGET_PACKAGES=/usr/nuget/packages

WORKDIR /app

ADD ./Conduit.sln /app/
ADD ./src/Conduit/Conduit.csproj /app/src/Conduit/
ADD ./tests/Conduit.IntegrationTests/Conduit.IntegrationTests.csproj /app/tests/Conduit.IntegrationTests/

RUN dotnet restore

# Build the app:
COPY . /app
RUN cd /app/src/Conduit && dotnet publish --configuration Release

################################################################################
# STAGE II: PUBLISHED
FROM microsoft/dotnet:runtime

COPY --from=builder /app/src/Conduit/bin/Release/netcoreapp2.0/publish /app

WORKDIR /app

CMD ["dotnet", "Conduit.dll"]
