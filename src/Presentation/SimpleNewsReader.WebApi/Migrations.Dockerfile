FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
COPY src/Presentation/SimpleNewsReader.WebApi/* /Presentation/SimpleNewsReader.WebApi/
COPY src/Domain/SimpleNewsReader.Domain/* /Domain/SimpleNewsReader.Domain/
COPY src/Infrastructure/SimpleNewsReader.Infrastructure/* /Infrastructure/SimpleNewsReader.Infrastructure/
COPY src/Application/SimpleNewsReader.Application/* /Application/SimpleNewsReader.Application/

RUN dotnet tool install --global dotnet-ef

RUN dotnet restore /Presentation/SimpleNewsReader.WebApi/SimpleNewsReader.WebApi.csproj


RUN /root/.dotnet/tools/dotnet-ef migrations add InitialMigrations --project /Infrastructure/SimpleNewsReader.Infrastructure/SimpleNewsReader.Infrastructure.csproj --startup-project /Presentation/SimpleNewsReader.WebApi/SimpleNewsReader.WebApi.csproj

RUN chmod +x /Presentation/SimpleNewsReader.WebApi/setup.sh
CMD /bin/bash /Presentation/SimpleNewsReader.WebApi/setup.sh