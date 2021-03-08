#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 81
ENV ASPNETCORE_URLS=http://+:81

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Tests_CRUD_API/Tests_CRUD_API.csproj", "Tests_CRUD_API/"]
COPY ["Tests_CRUD_DAL/Tests_CRUD_DAL.csproj", "Tests_CRUD_DAL/"]
COPY ["Tests_CRUD_BLL/Tests_CRUD_BLL.csproj", "Tests_CRUD_BLL/"]
RUN dotnet restore "Tests_CRUD_API/Tests_CRUD_API.csproj"
COPY . .
WORKDIR "/src/Tests_CRUD_API"
RUN dotnet build "Tests_CRUD_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tests_CRUD_API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Tests_CRUD_API.dll"]