# 1: Start from microsoft/dotnet:sdk as base:
FROM microsoft/dotnet:sdk

# 2: Set /app as the Home Directory, and set the NuGet download directory:
ENV HOME=/app NUGET_PACKAGES=/usr/nuget/packages

# 3: Set /app as the working directory:
WORKDIR /app

# 4: Copy the DotNet project file so we can download the dependency packages:
ADD ./MvcMovie.csproj /app/

# 5: Restore the project's dependency packages:
RUN dotnet restore
