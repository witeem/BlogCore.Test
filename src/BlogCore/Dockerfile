#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/BlogCore/BlogCore.csproj", "src/BlogCore/"]
COPY ["src/BlogCore.EFCore/BlogCore.EFCore.csproj", "src/BlogCore.EFCore/"]
COPY ["src/BlogCore.Core/BlogCore.Core.csproj", "src/BlogCore.Core/"]
COPY ["src/BlogCore.Extended/BlogCore.Extended.csproj", "src/BlogCore.Extended/"]
COPY ["src/BlogCore.Application/BlogCore.Application.csproj", "src/BlogCore.Application/"]
COPY ["src/BlogCore.Domain/BlogCore.Domain.csproj", "src/BlogCore.Domain/"]
RUN dotnet restore "src/BlogCore/BlogCore.csproj"
COPY . .
WORKDIR "/src/src/BlogCore"
RUN dotnet build "BlogCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlogCore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogCore.dll"]