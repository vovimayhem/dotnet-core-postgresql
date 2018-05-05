#! /bin/sh

# 1: Ensure we've got all the dependencies
dotnet restore

# 2: Run the given command:
exec "$@"
