#! /bin/sh

# The Docker App Container's development entrypoint.
# This is a script used by the project's Docker development environment to
# setup the app containers and databases upon runnning.

# 1: Run the app setup only if the requested command requires it:
if [ "$1" = "dotnet" ]
then
  # 2: Ensure we've got all the dependencies
  echo "=== Restoring packages..."
  dotnet restore

  if [ "$2" = "watch" ] && [ "$3" = "run" ]
  then
    # 3: Ensure we've got our database up to date:
    echo "=== Updating the database..."
    dotnet ef database update
  fi
fi

# 2: Run the given command:
exec "$@"
