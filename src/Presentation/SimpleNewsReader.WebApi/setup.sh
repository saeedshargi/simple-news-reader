#!/bin/bash

set -e

until /root/.dotnet/tools/dotnet-ef database update --project /src/Infrastructure/SimpleNewsReader.Infrastructure/SimpleNewsReader.Infrastructure.csproj --startup-project /src/Presentation/SimpleNewsReader.WebApi/SimpleNewsReader.WebApi.csproj  --no-build -- --environment Production; do
>&2 echo "SQL Server is starting up"
sleep 1
done

>&2 echo "SQL Server is up - executing command"