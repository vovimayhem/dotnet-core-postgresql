#! /bin/sh

# 1: Ensure we've got all the dependencies
echo "=== Restoring packages..."
dotnet restore

# 2: Ensure we've got our database up to date:
echo "=== Updating the database..."
dotnet ef database update

# 2: Run the given command:
exec "$@"
