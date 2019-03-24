# Stage I: Development =========================================================
# 1: Start from microsoft/dotnet:sdk as base:
FROM microsoft/dotnet:sdk AS development

# 2: Set /app as the Home Directory, and set the NuGet download directory:
ENV HOME=/app NUGET_PACKAGES=/usr/nuget/packages

# 3: Set /app as the working directory:
WORKDIR /app

# 4: Copy the DotNet project file so we can download the dependency packages:
ADD ./MvcMovie.csproj /app/

# 5: Restore the project's dependency packages:
RUN dotnet restore

# Stage II: Builder ============================================================
FROM development AS builder

# 1: Build the app:
COPY . /app
RUN dotnet publish --configuration Release

# Stage III: Release ===========================================================
FROM microsoft/dotnet:runtime AS release

COPY --from=builder /app/bin/Release/netcoreapp2.0/publish /app

WORKDIR /app

CMD [ "dotnet", "MvcMovie.dll" ]
