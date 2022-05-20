#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY src/Presentation/SimpleNewsReader.WebApi/* /Presentation/SimpleNewsReader.WebApi/
COPY src/Domain/SimpleNewsReader.Domain/* /Domain/SimpleNewsReader.Domain/
COPY src/Infrastructure/SimpleNewsReader.Infrastructure/* /Infrastructure/SimpleNewsReader.Infrastructure/
COPY src/Application/SimpleNewsReader.Application/* /Application/SimpleNewsReader.Application/
RUN dotnet restore "/Presentation/SimpleNewsReader.WebApi/SimpleNewsReader.WebApi.csproj"
COPY . .
WORKDIR /src
RUN dotnet clean "/Presentation/SimpleNewsReader.WebApi/SimpleNewsReader.WebApi.csproj"
RUN dotnet build "/Presentation/SimpleNewsReader.WebApi/SimpleNewsReader.WebApi.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "/Presentation/SimpleNewsReader.WebApi/SimpleNewsReader.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleNewsReader.WebApi.dll"]